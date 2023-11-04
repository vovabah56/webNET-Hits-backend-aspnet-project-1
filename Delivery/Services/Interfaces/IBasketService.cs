namespace Delivery.Services.Interfaces;

public interface IBasketService
{
    Task AddDishToCart(Guid userId, Guid dishId);
}