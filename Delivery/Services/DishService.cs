using AutoMapper;
using Delivery.Data.Models;
using Delivery.DB;
using Delivery.DTO;
using Delivery.Services.Interfaces;
using DeliveryBackend.DTO;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Services;

public class DishService : IDishService
{
    
    private ApplicationDbContext _context { get; set; }
    private IMapper _mapper { get; set; }

    public DishService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    

    public async Task AddDish(DishDto dishDtos)
    {
        await _context.Dishes.AddAsync(new Dish
        {
            Id = Guid.NewGuid(),
            Category = dishDtos.Category,
            Description = dishDtos.Description,
            IsVegetarian = dishDtos.Vegetarian,
            Name = dishDtos.Name,
            Photo = dishDtos.Image,
            Price = dishDtos.Price
        });
        await _context.SaveChangesAsync();
    }


    
    
}