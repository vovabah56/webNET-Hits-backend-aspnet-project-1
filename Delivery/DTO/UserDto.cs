using System.ComponentModel.DataAnnotations;

namespace Delivery.DTO;

public class UserDto
{
    public Guid Id { get; set; }
    
    [Required]
    public string FullName { get; set; }
    
    public DateTime? BirthTime { get; set; }
    
    [Required]
    public string Gender { get; set; }
    
    public string Address { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }
    
    [Phone]
    public string Phone { get; set; }
}