using Microsoft.AspNetCore.Authorization;

namespace Delivery.Services.ValidateTokenPolicy;

public class ValidateTokenRequirement : IAuthorizationRequirement
{
    public ValidateTokenRequirement()
    {
    }
}