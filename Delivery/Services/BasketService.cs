using AutoMapper;
using Delivery.Data.Models;
using Delivery.DB;
using Delivery.DTO;
using Delivery.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Services;

public class BasketService: IBasketService
{
    public ApplicationDbContext _context { get; set; }
    public IMapper _mapper { get; set; }

    public BasketService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task AddDishToCart(Guid userId, Guid dishId)
    {
        var dish = await _context.Dishes.FirstOrDefaultAsync(x => x.Id == dishId);

        if (dish == null)
        {
            var ex = new Exception();
            ex.Data.Add(StatusCodes.Status404NotFound.ToString(), "Dish not Found");
            throw ex;
        }

        var dishInCart = await _context.Carts
            .Where(x => x.DishId == dishId && x.UserId == userId && x.OrderId == null)
            .FirstOrDefaultAsync();

        if (dishInCart == null)
        {
            await _context.Carts.AddAsync(new Cart
                {
                    Count = 1,
                    DishId = dishId,
                    UserId = userId,
                    Id = Guid.NewGuid(),
                    OrderId = null
                }
            );
            await _context.SaveChangesAsync();
        }
        else
        {
            dishInCart.Count++;
            await _context.SaveChangesAsync();
        }
        
    }

    public async Task<List<BasketDto>> GetBasketUser(Guid userId)
    {
        var dishList = await _context.Carts.Where(x => x.UserId == userId && x.OrderId == null).Join(
                _context.Dishes,
                c => c.DishId,
                d => d.Id,
                (c, d) => new BasketDto
                {
                    Id = c.Id,
                    Name = d.Name,
                    Price = d.Price,
                    TotalPrice = d.Price * c.Count,
                    Count = c.Count,
                    Photo = d.Photo
                }
            )
            .ToListAsync();

        return dishList;
    }

    public async Task RemoveDishFromCart(Guid userId, Guid dishId)
    {
        if (await _context.Dishes.FirstOrDefaultAsync(x => x.Id == dishId) == null)
        {
            var ex = new Exception();
            ex.Data.Add(StatusCodes.Status400BadRequest.ToString(),
                "Dish not exists"
            );
            throw ex;
        }
        
        var dishCartEntity =
            await _context.Carts.Where(x => x.UserId == userId && x.DishId == dishId && x.OrderId == null).FirstOrDefaultAsync();

        if (dishCartEntity == null)
        {
            var ex = new Exception();
            ex.Data.Add(StatusCodes.Status404NotFound.ToString(),
                "Dish not found in cart"
            );
            throw ex;
        }

        dishCartEntity.Count--;
        if (dishCartEntity.Count == 0)
            _context.Carts.Remove(dishCartEntity);
        await _context.SaveChangesAsync();
    }
}