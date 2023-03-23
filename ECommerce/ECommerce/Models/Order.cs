using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models;

public class Order : IBaseModel
{
    [Key]
    public Guid Id { get; set; }

    public int Quantities { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime OrderCreatedAt { get; set; }
}