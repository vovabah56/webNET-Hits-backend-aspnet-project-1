﻿using AutoMapper;
using Delivery.Data.Models;
using Delivery.DB;
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
}