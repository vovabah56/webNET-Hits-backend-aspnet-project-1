﻿using Delivery.DB.Enums;

namespace Delivery.DTO;

public class MenuQuery
{
    public List<string> Categories { get; set; } = new ();
    public bool? Vegetarian { get; set; } = null;
    public DishSorting? Sorting { get; set; } = null;
    public int Page { get; set; } = 1;
}