using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Delivery.Data.Models;

public class Rating
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public Guid DishId { get; set; }
    
    [Required]
    public Guid UserId { get; set; }
    
    
    [Required]
    public int Value { get; set; }
    
    
}