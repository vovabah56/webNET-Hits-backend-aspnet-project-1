﻿using System.ComponentModel.DataAnnotations;

namespace Delivery.Data.Models;

public class Dish
{
    [Key]
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public double Price { get; set; }
    
    public string Description { get; set; }
    
    public double? Rating { get; set; }
    public bool IsVegetarian { get; set; }
    
    public string Category { get; set; }
    
    public string Photo { get; set; }
    
    
    public List<Cart> Carts { get; set; }
    public List<Rating> Ratings { get; set; }
    
}