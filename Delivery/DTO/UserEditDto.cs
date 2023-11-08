﻿using System.ComponentModel.DataAnnotations;

namespace Delivery.DB.Models;

public class UserEditDto
{
    [Required]
    [MinLength(1)]
    public string FullName { get; set; }
    public DateTime? BirthDate { get; set; }
    [Required]
    public string Gender { get; set; }
    public string? Address { get; set; }
    [Phone]
    public string? PhoneNumber { get; set; }
}