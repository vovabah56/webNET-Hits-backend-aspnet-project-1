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
    
    [HttpGet]
    [Authorize]
    [Authorize(Policy = "ValidateToken")]
    [Route("{id}")]
    public async Task<OrderDto> GetOrderInfo(Guid id)
    {
        return await _orderService.GetInfoOrder(Guid.Parse(User.Identity.Name), id);
    }

    [HttpGet]
    [Authorize]
    [Authorize(Policy = "ValidateToken")]
    public async Task<List<OrderInfoDto>> GetOrders()
    {
        return await _orderService.GetOrders(Guid.Parse(User.Identity.Name));
    }

    [HttpPost]
    [Authorize]
    [Authorize(Policy = "ValidateToken")]
    [Route("{id}/status")]
    public async Task ConfirmOrderDelivery(Guid id)
    {
        await _orderService.ConfirmOrderDel(Guid.Parse(User.Identity.Name), id);
    }
    
}