﻿using System.ComponentModel.DataAnnotations;

namespace Delivery.Data.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [MinLength(1)]
    public string FullName { get; set; }
    
    public DateTime? BirthDate { get; set; }
    
    [Required]
    public string Gender { get; set; }
    
    [Phone]
    public string? PhoneNumber { get; set; }
    
    [Required]
    [MinLength(1)]
    [EmailAddress]
    public string Email { get; set; }
    
    public string? Address { get; set; }
    
    [Required]
    [MinLength(6)]
    public string Password { get; set; }
}