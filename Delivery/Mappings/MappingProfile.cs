using AutoMapper;
using Delivery.Data.Models;
using Delivery.DB.Models;
using Delivery.DTO;

namespace Delivery.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserRegisterDto, User>();
        CreateMap<User, UserDto>();
    }
}