using Delivery.DTO;
using Delivery.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Controllers;


[ApiController]
[Route("api/dish")]
public class DishController : ControllerBase
{
    public IDishService _dishService { get; set; }


    public DishController(IDishService dishService)
    {
        _dishService = dishService;
    }


    [HttpGet]
    [Route("{id}")]
    public async Task<DishDto> GetDish(Guid id)
    {
        return await _dishService.GetDish(id);
    }
    
    
    [HttpPost]
    public async Task AddDish([FromBody] DishDto dishDtos)
    {
        await _dishService.AddDish(dishDtos);
    }
    
    
}