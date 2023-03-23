using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models;

public class OrderProduct : IBaseModel
{
    [Key] public Guid Id { get; set; }

    public Order? Order { get; set; }

    public Product? Product { get; set; }
}