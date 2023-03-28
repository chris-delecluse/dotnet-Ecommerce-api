using System.Security.Claims;
using ECommerce.Dto;
using ECommerce.Exceptions;
using ECommerce.Helpers;
using ECommerce.Models;
using ECommerce.Repositories;

namespace ECommerce.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly UserRepository _userRepository;

    public AuthService(IConfiguration configuration, UserRepository userRepository)
    {
        _configuration = configuration;
        _userRepository = userRepository;
    }

    public async Task<ResTokensDto> LocalSignin(AuthDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Email))
            throw RequestProblemException.ForMissingField(nameof(dto.Email));
        
        if (string.IsNullOrWhiteSpace(dto.Password))
            throw RequestProblemException.ForMissingField(nameof(dto.Password));

        User? user = await ValidateUser(dto)!;

        string accessToken = new JwtTokenBuilder(_configuration.GetValue<string>("Jwt:Key")!)
            .AddIssuer(_configuration.GetValue<string>("Jwt:Issuer")!)
            .AddAudience(_configuration.GetValue<string>("Jwt:Audience")!)
            .AddClaim(ClaimTypes.NameIdentifier, user.Id.ToString())
            .AddClaim(ClaimTypes.Email, user.Email!)
            .AddClaim(ClaimTypes.Role, "user role blabla...test!")
            .AddClaim("csrfToken", "helloworldtest")
            .SetExpiration(DateTime.Now.AddMinutes(30))
            .Build();

        return new ResTokensDto(accessToken, "refreshToken");
    }

    public async Task<User>? ValidateUser(AuthDto dto)
    {
        User? user = await _userRepository.GetOneByEmail(dto.Email!);

        if (user is null)
            throw RequestProblemException.ForInvalidCredentials();

        if (!HashHandler.VerifyPassword(dto.Password!, user.Password!, user.Salt!))
            throw RequestProblemException.ForInvalidCredentials();

        return user;
    }
}