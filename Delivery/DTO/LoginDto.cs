using System.ComponentModel.DataAnnotations;

namespace Delivery.DTO;

public class LoginDto
{
    [Required]
    [MinLength(1)]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [MinLength(1)]
    public string Password { get; set; }
}