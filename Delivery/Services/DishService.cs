using AutoMapper;
using Delivery.Data.Models;
using Delivery.DB;
using Delivery.DTO;
using Delivery.Services.Interfaces;

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

    public async Task<DishDto> GetDish(Guid id)
        {
            var dish = await _context.Dishes
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
    
            if (dish != null)
            {
                
                return _mapper.Map<DishDto>(dish);
            }
    
            var ex = new Exception();
            ex.Data.Add(StatusCodes.Status404NotFound.ToString(), "Dish not found.");
            throw ex;
        }

    public async Task AddDish(DishDto dishDtos)
    {
        await _context.Dishes.AddAsync(new Dish
        {
            Id = Guid.NewGuid(),
            Category = dishDtos.Category,
            Description = dishDtos.Description,
            IsVegetarian = dishDtos.IsVegetarian,
            Name = dishDtos.Name,
            Photo = dishDtos.Photo,
            Price = dishDtos.Price
        });
        await _context.SaveChangesAsync();
    }


    
    
}