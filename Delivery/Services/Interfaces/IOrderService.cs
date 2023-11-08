using Delivery.DTO;

namespace Delivery.Services.Interfaces;

public interface IOrderService
{
    public Task CreateOrder(Guid userId, CreateOrderDto orderCreateDto);
}