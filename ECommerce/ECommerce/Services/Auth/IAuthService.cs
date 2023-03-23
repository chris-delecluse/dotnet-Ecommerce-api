using System;
using ECommerce.Dto;

namespace ECommerce.Services;

public interface IAuthService
{
    Task<ResAuthDto> LocalSignin(AuthDto dto);
}