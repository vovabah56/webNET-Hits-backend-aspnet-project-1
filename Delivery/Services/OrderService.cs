using Delivery.Data.Models;
using Delivery.DB;
using Delivery.DB.Enums;
using Delivery.DB.Models;
using Delivery.DTO;
using Delivery.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Delivery.Services;

public class OrderService: IOrderService
{
    public ApplicationDbContext _context { get; set; }


    public OrderService(ApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext;
    }


    public async Task CreateOrder(Guid userId, CreateOrderDto orderCreateDto)
    {
        if (orderCreateDto.DeliveryTime - DateTime.Now < TimeSpan.FromMinutes(5) ||
            orderCreateDto.DeliveryTime - DateTime.Now > TimeSpan.FromHours(24))
        {
            var ex = new Exception();
            ex.Data.Add(StatusCodes.Status400BadRequest.ToString(),
                "Bad request, Delivery time range is 5m - 24h"
            );
            throw ex;
        }

        var cartDishes = await _context.Carts
            .Where(x => x.UserId == userId && x.OrderId == null)
            .ToListAsync();
        if (cartDishes.IsNullOrEmpty())
        {
            var ex = new Exception();
            ex.Data.Add(StatusCodes.Status404NotFound.ToString(),
                "Dishes in cart Not Found"
            );
            throw ex;
        }

       
        var orderId = Guid.NewGuid();
        var newOrder = new Order
        {
            Id = orderId,
            DeliveryTime = orderCreateDto.DeliveryTime,
            OrderTime = DateTime.UtcNow,
            Status = OrderStatus.InProcess.ToString(),
            Price = 0,
            Address = orderCreateDto.Address,
            UserId = userId
        };
        await _context.Orders.AddAsync(newOrder);
        await _context.SaveChangesAsync();
        newOrder.Price = await CreateOrderOperations(orderId, cartDishes);
        await _context.SaveChangesAsync();
    }

    private async Task<double> CreateOrderOperations(Guid orderId, List<Cart> cartDishes)
    {
        double res = 0;

        for (var i = 0; i < cartDishes.Count; i++)
        {
            cartDishes[i].OrderId = orderId;
            var dish = await _context.Dishes.FirstOrDefaultAsync(x => x.Id == cartDishes[i].DishId);
            if (dish == null)
            {
                var ex = new Exception();
                ex.Data.Add(StatusCodes.Status404NotFound.ToString(),
                    "Dish in Order not found"
                );
                throw ex;
            }

            res += cartDishes[i].Count * dish.Price;
        }

        await _context.SaveChangesAsync();

        return res;
    }
}