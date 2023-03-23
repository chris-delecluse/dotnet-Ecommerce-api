using System;
namespace ECommerce.Models;

public interface IBaseModel
{
    public Guid Id { get; set; }
}