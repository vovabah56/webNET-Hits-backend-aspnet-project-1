using Delivery.DTO;

namespace Delivery.Services.Interfaces;

public interface IUserService
{
    Task<TokenResponse> RegisterUser(UserRegisterDto userRegisterDto);
    Task<TokenResponse> LoginUser(LoginDto credentials);
}