using Delivery.DB;
using Delivery.DTO;
using Delivery.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Delivery.Controllers;

[ApiController]
[Route("api/order")]
public class OrderController: ControllerBase
{
    public IOrderService _orderService { get; set; }

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    
    [HttpPost]
    [Authorize]
    [Authorize(Policy = "ValidateToken")]
    public async Task CreateOrder([FromBody] CreateOrderDto orderCreateDto)
    {
        await _orderService.CreateOrder(Guid.Parse(User.Identity.Name), orderCreateDto);
    }
}