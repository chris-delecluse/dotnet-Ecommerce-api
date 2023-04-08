using System;
using ECommerce.Dto;
using ECommerce.Exceptions;
using ECommerce.Models;
using ECommerce.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;

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
    [Authorize]
    public async Task<ActionResult<QueryDto<List<User>>>> GetAll()
    {
        IEnumerable<User> users = await _userService.GetAll();

        QueryDto<List<User>> response = new(users.ToList(), users.Count());

        return Ok(response);
    }

    [HttpGet("{email}")]
    [Authorize]
    public async Task<ActionResult<QueryDto<User>>> GetOneByEmail(string email)
    {
        try
        {
            User? user = await _userService.GetOneByEmail(email);

            QueryDto<User> response = new(user!, 1);

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
    [Authorize]
    public async Task<ActionResult<MutationDto<User>>> Create(UserCreationDto creationDto)
    {
        try
        {
            User newUser = await _userService.InsertOne(creationDto);

            MutationDto<User> response = new("Resource added successfully", newUser);

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