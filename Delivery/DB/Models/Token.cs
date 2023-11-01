using System.ComponentModel.DataAnnotations;

namespace Delivery.DB.Models;

public class Token
{
    [Required]
    public string InvalidToken { get; set; }
    [Required]
    public DateTime ExpiredDate { get; set; }
}