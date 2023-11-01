using Delivery.DTO;
using Delivery.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Controllers;


[ApiController]
[Route("api/dish")]
public class DishController : ControllerBase
{
    public IDishService _DishService { get; set; }


    public DishController(IDishService dishService)
    {
        _DishService = dishService;
    }

    [HttpPost]
    public async Task AddDish([FromBody] DishDto dishDtos)
    {
        await _DishService.AddDish(dishDtos);
    }
    
    
}