using Delivery.DTO;
using DeliveryBackend.DTO;

namespace Delivery.Services.Interfaces;

public interface IDishService
{
    
    Task AddDish(DishDto dishDtos);



}