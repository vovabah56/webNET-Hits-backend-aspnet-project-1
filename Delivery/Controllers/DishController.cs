﻿using Delivery.DTO;
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

    [HttpGet]
    public async Task<DishMenuDto> GetMenu([FromQuery] MenuQuery menuQuery)
    {
        return await _dishService.GetMenu(menuQuery);
    }
    
    [HttpPost]
    public async Task AddDishs([FromBody] List<DishDto> dishDtos)
    {
        await _dishService.AddDishs(dishDtos);
    }
    
    
}