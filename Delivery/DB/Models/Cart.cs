using System.ComponentModel.DataAnnotations;

namespace Delivery.Data.Models;

public class Cart
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public Guid UserId { get; set; }
    
    [Required]
    public Guid DishId { get; set; }
    
    [Required]
    public int Count { get; set; }
    
    public Guid? OrderId { get; set; }
    
    
}