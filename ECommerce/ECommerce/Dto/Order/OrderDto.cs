namespace ECommerce.Dto;

public record OrderDto(IEnumerable<Guid> ProductIds);