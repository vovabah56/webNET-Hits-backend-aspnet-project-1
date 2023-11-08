using System.ComponentModel.DataAnnotations;

namespace Delivery.DTO;

public class CreateOrderDto
{
    public DateTime? DeliveryTime { get; set; }
    
    public string? Address { get; set; }
}