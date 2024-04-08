using System.ComponentModel.DataAnnotations;

namespace Delivery.DTO;

public class DishMenuDto
{
    [Required] 
    public List<DishDto> Dishes { get; set; }
    [Required]
    public PageInfoDto Pagination { get; set; }
}