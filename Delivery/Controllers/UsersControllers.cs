using Delivery.DB.Models;
using Delivery.DTO;
using Delivery.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DeliveryBackend.Controllers;

[ApiController]
[Route("api/account")]
public class UsersController : ControllerBase
{
    private readonly IUserService _usersService;

    public UsersController(IUserService usersService)
    {
        _usersService = usersService;
    }

    [HttpPost]
    [Route("register")]
    [SwaggerOperation(Summary = "Register new user")]
    public async Task<TokenResponse> RegisterUser([FromBody] UserRegisterDto userRegisterDto)
    {
        return await _usersService.RegisterUser(userRegisterDto);
    }

    [HttpPost]
    [Route("login")]
    public async Task<TokenResponse> Login([FromBody] LoginDto credentials)
    {
        return await _usersService.LoginUser(credentials);
    }


    [HttpPost]
    [Route("logout")]
    public async Task Logout()
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        if (token == null)
        {
            throw new Exception("Token not found");
        }

        await _usersService.LogoutUser(token);
    }

    [HttpPut]
    [Authorize]
    [Authorize(Policy = "ValidateToken")]
    [Route("profile")]
    public async Task EditProfile([FromBody] UserEditDto userEditDto)
    {
        await _usersService.EditUserProfile(Guid.Parse(User.Identity.Name), userEditDto);
    }
    
    
}