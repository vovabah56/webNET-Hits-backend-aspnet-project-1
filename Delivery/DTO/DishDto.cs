using System.ComponentModel.DataAnnotations;


namespace Delivery.DTO;

public class DishDto
{
    public Guid Id { get; set; }
    [Required]
    [MinLength(1)]
    public string Name { get; set; }
    [Required] 
    public double Price { get; set; }
    public string? Description { get; set; }
    [Required]
    public bool IsVegetarian { get; set; }
    [Required] 
    public string Category { get; set; }
    public string? Photo { get; set; }
    
    public double? Rating { get; set; }
}