namespace Delivery.DTO;

public class OrderDto
{
    public Guid Id { get; set; }
    
    public DateTime DeliveryTime { get; set; }
   
    public DateTime OrderTime { get; set; }
    public string Status { get; set; }
    
    public double Price { get; set; }
    
    public List<BasketDto?> Dishes { get; set; }
    
    public string Address { get; set; }
}