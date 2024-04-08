namespace Delivery.DTO;

public class BasketDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int Count { get; set; }
    public double TotalPrice { get; set; }
    public string Photo { get; set; }
    
    
}