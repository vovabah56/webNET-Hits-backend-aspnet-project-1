using System.ComponentModel.DataAnnotations;

namespace Delivery.Data.Models;

public class Token
{
    [Required]
    public string InvalidToken { get; set; }
    [Required]
    public DateTime ExpiredDate { get; set; }
    
}