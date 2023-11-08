using Delivery.DTO;

namespace Delivery.Services.Interfaces;

public interface IBasketService
{
    Task AddDishToCart(Guid userId, Guid dishId);
    Task<List<BasketDto>> GetBasketUser(Guid userOd);
    Task RemoveDishFromCart(Guid userId, Guid dishId);
}