using Delivery.DTO;

namespace Delivery.Services.Interfaces;

public interface IDishService
{
    Task<DishDto> GetDish(Guid id);
    Task AddDish(DishDto dishDtos);



}