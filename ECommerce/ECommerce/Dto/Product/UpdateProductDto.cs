using System;
namespace ECommerce.Dto;

public record UpdateProductDto(string? Name, string? Description, double? Price);