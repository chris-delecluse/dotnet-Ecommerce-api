using System;
namespace ECommerce.Dto;

public record MutationDto<T>(string Message, T Result);