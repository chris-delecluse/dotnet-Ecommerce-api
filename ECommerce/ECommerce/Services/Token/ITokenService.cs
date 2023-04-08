using ECommerce.Dto;
using ECommerce.Models;

namespace ECommerce.Services;

public interface ITokenService
{
    string GenerateAccessToken(User user);
    
    
    Task<AuthTokenDto> RefreshToken();
    Task<AuthTokenDto> GetTokens(HttpResponse httpResponse);


    // amelioration
    string GenerateRefreshToken(); 
    
    void SetCookieRefreshToken(HttpResponse httpResponse, string refreshToken);
    string GetCookieRefreshToken(HttpRequest httpRequest);
    
    Task<RefreshToken?> GetOneByRefreshToken(string refreshToken);
    Task<RefreshToken> InsertOne(RefreshToken refreshToken);
}