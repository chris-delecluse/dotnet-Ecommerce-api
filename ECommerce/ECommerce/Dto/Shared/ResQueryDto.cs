using System;
namespace ECommerce.Dto;

public record ResQueryDto<T>(T Result, int Count);