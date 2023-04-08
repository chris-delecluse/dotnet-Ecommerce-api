using System.Net;
using ECommerce.Dto;
using ECommerce.Exceptions;
using ECommerce.Helpers.Attributes;
using ECommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers;

[ApiController]
[Route("api/token")]
public class TokenController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public TokenController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }
    
    [HttpPost]
    [ValidateRefreshToken]
    [Route("refresh")]
    public async Task<ActionResult<AuthTokenDto>> RefreshToken(RefreshTokenDto dto)
    {
        try
        {
            // var accessToken = await _tokenService.GetAccessToken(Response);
            // await _tokenService.SetCookieRefreshToken(Response);
            
            AuthTokenDto tokens = await _tokenService.GetTokens(Response);

            return Ok(tokens);
        }
        catch (InvalidCredentialException e)
        {
            return Unauthorized(new { e.Message });
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new { e.Message });
        }
    }
}

// cookie pour gerer le refresh token
public record RefreshTokenDto(string Token);