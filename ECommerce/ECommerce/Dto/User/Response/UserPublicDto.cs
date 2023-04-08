namespace ECommerce.Dto;

public record UserPublicDto(
    Guid Id,
    string Firstname,
    string Lastname,
    string Email
);