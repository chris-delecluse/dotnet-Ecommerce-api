using ECommerce.Dto;
using ECommerce.Exceptions;
using ECommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers;

[ApiController]
[Route("api/authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthenticationController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<ResAuthDto>> LocalSignIn(AuthDto dto)
    {
        try
        {
            ResAuthDto response = await _authService.LocalSignin(dto);
            
            return Ok(response);
        }
        catch (BadRequestException e)
        {
            return BadRequest(new { e.Message });
        }
        catch (InvalidCredentialException e)
        {
            return Unauthorized(new { e.Message });
        }
    }
}