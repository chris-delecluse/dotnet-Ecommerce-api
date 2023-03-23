using System;
using ECommerce.Dto;
using ECommerce.Exceptions;
using ECommerce.Models;
using ECommerce.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ECommerce.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<ResQueryDto<List<User>>>> GetAll()
    {
        IEnumerable<User> users = await _userService.GetAll();

        ResQueryDto<List<User>> response = new(users.ToList(), users.Count());

        return Ok(response);
    }

    [HttpGet("{email}")]
    public async Task<ActionResult<ResQueryDto<User>>> GetOneByEmail(string email)
    {
        try
        {
            User? user = await _userService.GetOneByEmail(email);

            ResQueryDto<User> response = new(user!, 1);

            return Ok(response);
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new { ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<ResMutationDto<User>>> Create(UserDto dto)
    {
        try
        {
            User newUser = await _userService.InsertOne(dto);

            ResMutationDto<User> response = new("Resource added successfully", newUser);

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
}