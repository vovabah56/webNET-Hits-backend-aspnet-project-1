using Delivery.DTO;

namespace Delivery.Services.Interfaces;

public interface IUserService
{
    Task<TokenResponse> LoginUser(LoginDto credentials);
}