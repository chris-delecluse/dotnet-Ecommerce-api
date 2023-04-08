namespace ECommerce.Dto;

public record QueryDto<T>(T Result, int Count);