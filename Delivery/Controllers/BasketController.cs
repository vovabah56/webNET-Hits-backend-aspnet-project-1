using Delivery.DTO;
using Delivery.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Controllers;

[ApiController]
[Route("api/basket")]
public class BasketController: ControllerBase
{
    public IBasketService _basketService { get; set; }

    public BasketController(IBasketService basketService)
    {
        _basketService = basketService;
    }

    [HttpPost]
    [Authorize]
    [Authorize(Policy = "ValidateToken")]
    [Route("dish/{dishId}")]
    public async Task AddDishToCart(Guid dishId)
    {
        await _basketService.AddDishToCart(Guid.Parse(User.Identity.Name), dishId);
    }


    [HttpGet]
    [Authorize]
    [Authorize(Policy = "ValidateToken")]
    public Task<List<BasketDto>> GetBasketUser()
    {
        return _basketService.GetBasketUser(Guid.Parse(User.Identity.Name));
    }
    
    [HttpDelete]
    [Authorize]
    [Authorize(Policy = "ValidateToken")]
    [Route("dish/{dishId}")]
    public async Task DecreaseDishQuantityInCart(Guid dishId)
    {
        await _basketService.RemoveDishFromCart(Guid.Parse(User.Identity.Name), dishId);
    }
}