using AutoMapper;
using Delivery.Data.Models;
using Delivery.DB.Models;
using Delivery.DTO;

namespace Delivery.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Dish, DishDto>().ReverseMap();
        CreateMap<UserRegisterDto, User>();
        CreateMap<User, UserDto>();
    }
}