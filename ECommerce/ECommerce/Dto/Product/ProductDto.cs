using System;
namespace ECommerce.Dto;

public record ProductDto(string Name, string Description, double Price, int Quantities);