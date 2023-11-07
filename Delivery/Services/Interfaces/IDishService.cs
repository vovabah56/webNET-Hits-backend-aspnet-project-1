using Delivery.DTO;

namespace Delivery.Services.Interfaces;

public interface IDishService
{
    Task<DishDto> GetDish(Guid id);
    Task AddDishs(List<DishDto> dishDtos);

    Task<DishMenuDto> GetMenu(MenuQuery menuQuery);

    Task SetRating(Guid dishId, Guid userId, int value);


}