﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace EcommerceAPI.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Image { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}