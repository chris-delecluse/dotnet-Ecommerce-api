namespace ECommerce.Dto;

public record UserPrivateDto(
    Guid Id,
    string Firstname,
    string Lastname,
    string Email,
    string Password
);