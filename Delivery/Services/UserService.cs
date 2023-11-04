using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using AutoMapper;
using Delivery.Configurations;
using Delivery.Data.Models;
using Delivery.DB;
using Delivery.DB.Enums;
using Delivery.DB.Models;
using Delivery.DTO;


using Delivery.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Delivery.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UserService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task LogoutUser(string token)
    {
        var alreadyExistsToken = await _context.Tokens.FirstOrDefaultAsync(x => x.InvalidToken == token);

        if (alreadyExistsToken == null)
        {
            var handler = new JwtSecurityTokenHandler();
            var expiredDate = handler.ReadJwtToken(token).ValidTo;
            _context.Tokens.Add(new Token { InvalidToken = token, ExpiredDate = expiredDate });
            await _context.SaveChangesAsync();
        }
        else
        {
            var ex = new Exception();
            ex.Data.Add(StatusCodes.Status401Unauthorized.ToString(),
                "Token is already invalid"
            );
            throw ex;
        }
    }

    public async Task<TokenResponse> RegisterUser(UserRegisterDto userRegisterDto)
    {
        userRegisterDto.Email = NormalizeAttribute(userRegisterDto.Email);

        await UniqueCheck(userRegisterDto);

        byte[] salt;
        RandomNumberGenerator.Create().GetBytes(salt = new byte[16]);
        var pbkdf2 = new Rfc2898DeriveBytes(userRegisterDto.Password, salt, 100000);
        var hash = pbkdf2.GetBytes(20);
        var hashBytes = new byte[36];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);
        var savedPasswordHash = Convert.ToBase64String(hashBytes);

        CheckGender(userRegisterDto.Gender);
        CheckBirthDate(userRegisterDto.BirthDate);

        await _context.Users.AddAsync(new User
        {
            Id = Guid.NewGuid(),
            FullName = userRegisterDto.FullName,
            BirthDate = userRegisterDto.BirthDate,
            Address = userRegisterDto.Address,
            Email = userRegisterDto.Email,
            Gender = userRegisterDto.Gender,
            Password = savedPasswordHash,
            PhoneNumber = userRegisterDto.PhoneNumber,
        });
        await _context.SaveChangesAsync();

        var credentials = new LoginDto
        {
            Email = userRegisterDto.Email,
            Password = userRegisterDto.Password
        };

        return await LoginUser(credentials);
    }
    
    
    
    
    public async Task<TokenResponse> LoginUser(LoginDto credentials)
    {
        credentials.Email = NormalizeAttribute(credentials.Email);

        var identity = await GetIdentity(credentials.Email, credentials.Password);

        var now = DateTime.UtcNow;

        var jwt = new JwtSecurityToken(
            issuer: JwtConfigurations.Issuer,
            audience: JwtConfigurations.Audience,
            notBefore: now,
            claims: identity.Claims,
            expires: now.AddMinutes(JwtConfigurations.Lifetime),
            signingCredentials: new SigningCredentials(JwtConfigurations.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256));

        var encodeJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        var result = new TokenResponse()
        {
            Token = encodeJwt
        };
        
        return result;
    }

    
    

    

    private async Task<ClaimsIdentity> GetIdentity(string email, string password)
    {
        var userEntity = await _context
            .Users
            .FirstOrDefaultAsync(x => x.Email == email);

        if (userEntity == null)
        {
            var ex = new Exception();
            ex.Data.Add(StatusCodes.Status401Unauthorized.ToString(),
                "User not exists"
            );
            throw ex;
        }

        if (!CheckHashPassword(userEntity.Password, password))
        {
            var ex = new Exception();
            ex.Data.Add(StatusCodes.Status401Unauthorized.ToString(),
                "Wrong password"
            );
            throw ex;
        }

        var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, userEntity.Id.ToString())
        };

        var claimsIdentity = new ClaimsIdentity
        (
            claims,
            "Token",
            ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType
        );

        return claimsIdentity;
    }

    private static bool CheckHashPassword(string savedPasswordHash, string password)
    {
        var hashBytes = Convert.FromBase64String(savedPasswordHash);
        var salt = new byte[16];
        Array.Copy(hashBytes, 0, salt, 0, 16);
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
        var hash = pbkdf2.GetBytes(20);
        for (var i = 0; i < 20; i++)
            if (hashBytes[i + 16] != hash[i])
                return false;
        return true;
    }

    private static string NormalizeAttribute(string value)
    {
        return value.ToLower().TrimEnd();
    }

    private async Task UniqueCheck(UserRegisterDto userRegisterModel)
    {
        var email = await _context
            .Users
            .Where(x => userRegisterModel.Email == x.Email)
            .FirstOrDefaultAsync();

        if (email != null)
        {
            var ex = new Exception();
            ex.Data.Add(StatusCodes.Status409Conflict.ToString(),
                $"Account with email '{userRegisterModel.Email}' already exists"
            );
            throw ex;
        }
    }
    
    
    private static void CheckGender(string gender)
    {
        if (gender == Gender.Male.ToString() || gender == Gender.Female.ToString()) return;

        var ex = new Exception();
        ex.Data.Add(StatusCodes.Status409Conflict.ToString(),
            $"Possible Gender values: {Gender.Male.ToString()}, {Gender.Female.ToString()}");
        throw ex;
    }

    private static void CheckBirthDate(DateTime? birthDate)
    {
        if (birthDate == null || birthDate <= DateTime.Now) return;

        var ex = new Exception();
        ex.Data.Add(StatusCodes.Status409Conflict.ToString(),
            "Birth date can't be later than today");
        throw ex;
    }
}