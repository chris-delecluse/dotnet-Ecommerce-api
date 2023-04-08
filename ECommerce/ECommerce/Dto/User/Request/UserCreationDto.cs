namespace ECommerce.Dto;

public record UserCreationDto(
    string Firstname,
    string Lastname,
    string Email,
    string Password
);