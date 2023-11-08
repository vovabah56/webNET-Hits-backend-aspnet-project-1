using AutoMapper;
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
    public IMapper _mapper { get; set; }


    public OrderService(ApplicationDbContext applicationDbContext, IMapper mapper)
    {
        _context = applicationDbContext;
        _mapper = mapper;
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
            DeliveryTime = DateTime.UtcNow,
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

    public async Task<OrderDto> GetInfoOrder(Guid userId, Guid orderId)
    {
        var orderInfo = await _context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
        if (orderInfo == null)
        {
            var ex = new Exception();
            ex.Data.Add(StatusCodes.Status404NotFound.ToString(),
                "Order Info not found"
            );
            throw ex;
        }

        if (orderInfo.UserId != userId)
        {
            var ex = new Exception();
            ex.Data.Add(StatusCodes.Status403Forbidden.ToString(),
                "Invalid order owner"
            );
            throw ex;
        }
        
        var orderCarts = await _context.Carts.Where(x => x.OrderId == orderId).ToListAsync();
        if (orderCarts.IsNullOrEmpty())
        {
            var ex = new Exception();
            ex.Data.Add(StatusCodes.Status404NotFound.ToString(),
                "Dishes in Order not found"
            );
            throw ex;
        }
        
        var dishes = new List<Dish>();
        foreach (var orderCart in orderCarts)
        {
            var dish = await _context.Dishes.FirstOrDefaultAsync(x => x.Id == orderCart.DishId);

            if (dish != null)
                dishes.Add(dish);
            else
            {
                var ex = new Exception();
                ex.Data.Add(StatusCodes.Status404NotFound.ToString(),
                    "Dish in Order not found"
                );
                throw ex;
            }
        }
        
        var convertedDishes = (from orderCart in orderCarts
            let dish = dishes.FirstOrDefault(x => x.Id == orderCart.DishId)
            where dish != null
            select new BasketDto
            {
                Id = dish.Id,
                Name = dish.Name,
                Price = dish.Price,
                TotalPrice = orderCart.Count * dish.Price,
                Count = orderCart.Count,
                Photo = dish.Photo
            }).ToList();

        if (!convertedDishes.IsNullOrEmpty())
            return new OrderDto
            {
                Id = orderInfo.Id,
                DeliveryTime = orderInfo.DeliveryTime,
                OrderTime = orderInfo.OrderTime,
                Status = orderInfo.Status,
                Price = orderInfo.Price,
                Dishes = convertedDishes,
                Address = orderInfo.Address
            };
        {
            var ex = new Exception();
            ex.Data.Add(StatusCodes.Status404NotFound.ToString(),
                "Empty order list returned"
            );
            throw ex;
        }
    }

    public async Task<List<OrderInfoDto>> GetOrders(Guid userId)
    {
            var orders = await _context.Orders.Where(x => x.UserId == userId).ToListAsync();

            return _mapper.Map<List<OrderInfoDto>>(orders);
    }

    public async Task ConfirmOrderDel(Guid userId, Guid orderId)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);

        if (order == null)
        {
            var ex = new Exception();
            ex.Data.Add(StatusCodes.Status404NotFound.ToString(),
                "Order Info not found"
            );
            throw ex;
        }

        if (order.UserId != userId)
        {
            var ex = new Exception();
            ex.Data.Add(StatusCodes.Status403Forbidden.ToString(),
                "Invalid order owner"
            );
            throw ex;
        }

        order.Status = OrderStatus.Delivered.ToString();
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