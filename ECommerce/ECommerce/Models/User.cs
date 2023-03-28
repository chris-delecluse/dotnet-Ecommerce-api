using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models;

public class User : IBaseModel
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Firstname is required")]
    public string? Firstname { get; set; }

    [Required(ErrorMessage = "Lastname is required")]
    public string? Lastname { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string? Email { get; set; }

    [MinLength(4)]
    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }

    public byte[]? Salt { get; set; }
    
    public IEnumerable<RefreshToken>? RefreshTokens { get; set; }
}