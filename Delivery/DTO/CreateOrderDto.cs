using System.ComponentModel.DataAnnotations;

namespace Delivery.DTO;

public class CreateOrderDto
{
    [Required]
    public DateTime DeliveryTime { get; set; }
    
    [Required]
    public string Address { get; set; }
}