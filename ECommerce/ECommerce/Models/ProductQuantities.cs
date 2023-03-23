using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models;

public class ProductQuantities : IBaseModel
{
    [Key]
    public Guid Id { get; set; }

    public int Quantities { get; set; }

    public Product? Product { get; set; }
}