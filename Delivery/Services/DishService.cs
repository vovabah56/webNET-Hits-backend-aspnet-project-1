using AutoMapper;
using Delivery.Data.Models;
using Delivery.DB;
using Delivery.DB.Enums;
using Delivery.DTO;
using Delivery.Services.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

    

    public async Task AddDishs(List<DishDto> dishDtos)
    {
        _context.Dishes.AddRange(_mapper.Map<List<Dish>>(dishDtos));
        await _context.SaveChangesAsync();
        
    }

    public async Task<DishMenuDto> GetMenu(MenuQuery menuQuery)
    {
        var menu = await GetDishesByDishListQuery(menuQuery);

        var dishesOrdered = SortMenu(menuQuery, menu);

        var dishes = dishesOrdered.Skip((menuQuery.Page - 1) * 5).Take(Range.EndAt(5)).ToList();

        var pagination = new PageInfoDto
        {
            Size = dishes.Count,
            Count = (menu.Count + 4) / 5,
            Current = menuQuery.Page
        };

        if (pagination.Current <= pagination.Count && pagination.Current > 0)
            return new DishMenuDto
            {
                Dishes = _mapper.Map<List<DishDto>>(dishes),
                Pagination = pagination
            };

        var ex = new Exception();
        ex.Data.Add(StatusCodes.Status400BadRequest.ToString(),
            "Invalid value for attribute page"
        );
        throw ex;
    }

    public async Task<bool> CheckRating(Guid dishId, Guid userId)
    {
        await CheckDishInDb(dishId);
        var getRatingUser =  await _context.Ratings.FirstOrDefaultAsync(x => x.DishId == dishId && x.UserId == userId);
        return getRatingUser == null && await IsDishOrdered(userId, dishId);
    }   

    private async Task<bool> IsDishOrdered(Guid userId, Guid dishId)
    {
        var carts = await _context.Carts.Where(x => x.DishId == dishId && x.UserId == userId && x.OrderId != null).ToListAsync();
        foreach (var cart in carts)
        {
            if (await _context.Orders.FirstOrDefaultAsync(x =>
                    x.UserId == userId 
                    && x.Id == cart.Id 
                    && x.Status == OrderStatus.Delivered.ToString()) != null)
            {
                return true;
            }
        }

        return false;
    }

    private async Task CheckDishInDb(Guid dishId)
    {
        if (await _context.Dishes.FirstOrDefaultAsync(x => x.Id == dishId) == null)
        {
            var ex = new Exception();
            ex.Data.Add(StatusCodes.Status404NotFound.ToString(), "Dish not found");
            throw ex;
        }
    }

    private static IEnumerable<Dish> SortMenu(MenuQuery dishListQuery, IEnumerable<Dish> dishList)
    {
        var orderBy = dishListQuery.Sorting;
        if (orderBy == DishSorting.NameAsc.ToString())
            return dishList.OrderBy(s => s.Name).ToList();
        if (orderBy == DishSorting.NameDesc.ToString())
            return dishList.OrderByDescending(s => s.Name).ToList();
        if (orderBy == DishSorting.PriceAsc.ToString())
            return dishList.OrderBy(s => s.Price).ToList();
        if (orderBy == DishSorting.PriceDesc.ToString())
            return dishList.OrderByDescending(s => s.Price).ToList();
        if (orderBy == DishSorting.RatingAsc.ToString())
            return dishList.OrderBy(s => s.Rating).ToList();
        return orderBy == DishSorting.RatingDesc.ToString()
            ? dishList.OrderByDescending(s => s.Rating).ToList()
            : dishList.OrderBy(s => s.Name).ToList();
    }
    
    
    private async Task<List<Dish>> GetDishesByDishListQuery(MenuQuery dishListQuery)
    {
        foreach (var category in dishListQuery.Categories)
        {
            if (category != DishCategory.Dessert.ToString()
                && category != DishCategory.Drink.ToString()
                && category != DishCategory.Soup.ToString()
                && category != DishCategory.Wok.ToString()
                && category != DishCategory.Pizza.ToString()
                && !category.IsNullOrEmpty())
            {
                var ex = new Exception();
                ex.Data.Add(StatusCodes.Status400BadRequest.ToString(),
                    $"Dish Category {category} is not available"
                );
                throw ex;
            }

            if (category.IsNullOrEmpty())
            {
                if (dishListQuery.Vegetarian == null)
                    return await _context.Dishes.ToListAsync();
                return await _context.Dishes.Where(x =>
                    dishListQuery.Vegetarian == x.IsVegetarian).ToListAsync();
            }
        }

        if (dishListQuery.Categories.IsNullOrEmpty())
        {
            if (dishListQuery.Vegetarian == null)
                return await _context.Dishes.ToListAsync();
            return await _context.Dishes.Where(x =>
                dishListQuery.Vegetarian == x.IsVegetarian).ToListAsync();
        }

        if (dishListQuery.Vegetarian == null)
            return await _context.Dishes.Where(x =>
                dishListQuery.Categories.Contains(x.Category)).ToListAsync();
        return await _context.Dishes.Where(x =>
            dishListQuery.Categories.Contains(x.Category) &&
            dishListQuery.Vegetarian == x.IsVegetarian).ToListAsync();
    }
}