using System;
namespace ECommerce.Dto;

public record OrderDto(IEnumerable<Guid> productIds);