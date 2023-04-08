using System.Net;
using ECommerce.Dto;
using ECommerce.Exceptions;
using ECommerce.Models;
using ECommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public AuthenticationController(IAuthService authService, IUserService userService)
    {
        _authService = authService;
        _userService = userService;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("local/sign-in")]
    public async Task<ActionResult<AuthTokenDto>> LocalSignIn(LoginDto dto)
    {
        try
        {
            AuthTokenDto response = await _authService.LocalSignin(HttpContext ,dto);

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

    [HttpPost]
    [AllowAnonymous]
    [Route("local/create-account")]
    public async Task<ActionResult<MutationDto<UserPublicDto>>> LocalSignUp(UserCreationDto creationDto)
    {
        try
        {
            User newUser = await _userService.InsertOne(creationDto);

            UserPublicDto userWithoutSensitiveData = new(
                Id: newUser.Id,
                Firstname: newUser.Firstname!,
                Lastname: newUser.Lastname!,
                Email: newUser.Email!
            );

            MutationDto<UserPublicDto> response = new("User created successfully", userWithoutSensitiveData);

            return Created($"/api/user/{newUser.Id}", response);
        }
        catch (BadRequestException ex)
        {
            return BadRequest(new { ex.Message });
        }
        catch (ConflictException ex)
        {
            return Conflict(new { ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new { ex.Message });
        }
    }

    [HttpPost]
    [Authorize]
    [Route("local/logout")]
    public async Task<ActionResult<MutationDto<User>>> LocalLogout()
    {
        return Ok();
    }

    [HttpPost]
    [Authorize]
    [Route("local/logout-user-from-all-sessions")]
    public async Task<ActionResult<MutationDto<User>>> LocalLogoutAll()
    {
        return Ok();
    }
}