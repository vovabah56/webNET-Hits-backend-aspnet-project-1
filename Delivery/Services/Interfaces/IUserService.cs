using Delivery.DTO;

namespace Delivery.Services.Interfaces;

public interface IUserService
{
    Task LogoutUser(string token);
    /*Task EditUserProfile(Guid guid, UserEditDto userEditDto);*/
    Task<TokenResponse> RegisterUser(UserRegisterDto userRegisterDto);
    Task<TokenResponse> LoginUser(LoginDto credentials);

    Task<UserDto> GetUserProfile(Guid guid);


}