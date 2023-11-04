using AutoMapper;
using Delivery.Data.Models;
using Delivery.DTO;

namespace Delivery.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserRegisterDto, User>();
        CreateMap<User, UserDto>();
        CreateMap<Dish, DishDto>().ReverseMap();
    }
}