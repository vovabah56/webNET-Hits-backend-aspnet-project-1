using Delivery.DTO;

namespace Delivery.Services.Interfaces;

public interface IOrderService
{
    public Task CreateOrder(Guid userId, CreateOrderDto orderCreateDto);
    public Task<OrderDto> GetInfoOrder(Guid userId, Guid orderI);
    public Task<List<OrderInfoDto>> GetOrders(Guid userId);
    public Task ConfirmOrderDel(Guid userId, Guid orderId);
}