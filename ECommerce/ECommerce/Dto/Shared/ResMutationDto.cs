using System;
namespace ECommerce.Dto;

public record ResMutationDto<T>(string Message, T Result);