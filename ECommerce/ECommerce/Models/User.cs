using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models;

public class User : IBaseModel
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string? Firstname { get; set; }

    [Required]
    public string? Lastname { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [MinLength(4)]
    public string? Password { get; set; }

    public byte[]? Salt { get; set; }
}