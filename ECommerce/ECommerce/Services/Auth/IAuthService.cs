using System;
using ECommerce.Dto;
using ECommerce.Models;

namespace ECommerce.Services;

public interface IAuthService
{
    Task<ResAuthDto> LocalSignin(AuthDto dto);
    public Task<User>? ValidateUser(AuthDto dto);
}