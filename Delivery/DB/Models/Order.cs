using System.ComponentModel.DataAnnotations;
using Delivery.Data.Models;

namespace Delivery.DB.Models;

public class Order
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public DateTime DeliveryTime { get; set; }
    
    [Required]
    public DateTime OrderTime { get; set; }
    
    public string Status { get; set; } 
    
    public double Price { get; set; }
    
    [Required]
    public string Address { get; set; }
    
    [Required]
    public Guid UserId { get; set; }
    
    public List<Cart> Carts { get; set; }
}