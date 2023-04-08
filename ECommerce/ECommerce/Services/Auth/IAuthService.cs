using System;
using ECommerce.Dto;
using ECommerce.Models;

namespace ECommerce.Services;

public interface IAuthService
{
    Task<AuthTokenDto> LocalSignin(HttpContext httpContext, LoginDto dto);
    Task<User>? ValidateUser(LoginDto dto);
    Task<User> LogoutUser();
    Task<User> LogoutUserFromAllSessions();
}