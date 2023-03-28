using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models;


public class RefreshToken: IBaseModel
{
    [Key]
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "Token field is requiered")]
    public string? Token { get; set; }
    
    public DateTime ExpireIn { get; set; }

    public User? User { get; set; }
}