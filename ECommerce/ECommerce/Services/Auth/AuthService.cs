using ECommerce.Dto;
using ECommerce.Exceptions;
using ECommerce.Helpers;
using ECommerce.Models;
using ECommerce.Repositories;

namespace ECommerce.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;


    public AuthService(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<AuthTokenDto> LocalSignin(HttpContext httpContext, LoginDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Email))
            throw RequestProblemException.ForMissingField(nameof(dto.Email));

        if (string.IsNullOrWhiteSpace(dto.Password))
            throw RequestProblemException.ForMissingField(nameof(dto.Password));

        User? user = await ValidateUser(dto)!;

        string accessToken = _tokenService.GenerateAccessToken(user);

        RefreshToken refreshToken = new()
        {
            User = user,
            Token = _tokenService.GenerateRefreshToken(),
            ExpireIn = DateTime.Now.AddDays(7)
        };

        await _tokenService.InsertOne(refreshToken);

        _tokenService.SetCookieRefreshToken(httpContext.Response, refreshToken.Token);

        return new AuthTokenDto(accessToken);
    }

    public async Task<User>? ValidateUser(LoginDto dto)
    {
        User? user = await _userRepository.GetOneByEmail(dto.Email!);

        if (user is null)
            throw RequestProblemException.ForInvalidCredentials();

        if (!HashHandler.VerifyPassword(dto.Password!, user.Password!, user.Salt!))
            throw RequestProblemException.ForInvalidCredentials();

        return user;
    }

    public Task<User> LogoutUser()
    {
        throw new NotImplementedException();
    }

    public Task<User> LogoutUserFromAllSessions()
    {
        throw new NotImplementedException();
    }
}