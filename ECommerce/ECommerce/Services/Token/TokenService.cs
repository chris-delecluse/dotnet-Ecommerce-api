using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ECommerce.Dto;
using ECommerce.Exceptions;
using ECommerce.Helpers;
using ECommerce.Models;
using ECommerce.Repositories;

namespace ECommerce.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly ITokenRepository _tokenRepository;

    public TokenService(IConfiguration configuration,ITokenRepository tokenRepository)
    {
        _configuration = configuration;
        _tokenRepository = tokenRepository;
    }

    public string GenerateAccessToken(User user)
    {
        return new JwtTokenBuilder(_configuration.GetValue<string>("Jwt:Key")!)
            .AddIssuer(_configuration.GetValue<string>("Jwt:Issuer")!)
            .AddAudience(_configuration.GetValue<string>("Jwt:Audience")!)
            .AddClaim(ClaimTypes.NameIdentifier, user.Id.ToString())
            .AddClaim(ClaimTypes.Email, user.Email!)
            .AddClaim(ClaimTypes.Role, "user role blabla...test!")
            .AddClaim("csrfToken", "helloworldtest")
            .SetExpiration(DateTime.Now.AddMinutes(30))
            .Build();
    }
    
    public string GenerateRefreshToken()
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("RefreshToken:Key")!);
        byte[] randomBytes = new byte[32];
        
        RandomNumberGenerator.Fill(randomBytes);

        using var hmac = new HMACSHA256(keyBytes);
        byte[] hashBytes = hmac.ComputeHash(randomBytes);
        
        return Convert.ToBase64String(hashBytes);
    }

    public async Task<AuthTokenDto> RefreshToken()
    {
        throw new NotImplementedException();
    }

    public async Task<AuthTokenDto> GetTokens(HttpResponse httpResponse)
    {
        var accessToken = "test access token";

        SetCookieRefreshToken(httpResponse, "");

        return new AuthTokenDto(accessToken);
    }

    public string GetCookieRefreshToken(HttpRequest httpRequest)
    {
        if (!httpRequest.Cookies.TryGetValue("refresh-token", out string refreshToken))
            throw RequestProblemException.ForInvalidCredentials();

        return refreshToken;
    }

    public void SetCookieRefreshToken(HttpResponse httpResponse, string refreshToken)
    {
        CookieOptions cookieOptions = new()
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.Now.AddDays(7)
        };

        httpResponse.Cookies.Append("refresh-token", refreshToken, cookieOptions);
    }

    // checker le catch
    public async Task<RefreshToken?> GetOneByRefreshToken(string refreshToken) =>
        await _tokenRepository.GetOneByToken(refreshToken) ?? throw RequestProblemException.ForInvalidCredentials();

    public async Task<RefreshToken> InsertOne(RefreshToken refreshToken) =>
        await _tokenRepository.InsertOne(refreshToken);
}