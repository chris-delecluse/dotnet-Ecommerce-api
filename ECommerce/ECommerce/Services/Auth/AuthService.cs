using System;
using ECommerce.Dto;
using ECommerce.Exceptions;
using ECommerce.Helpers;
using ECommerce.Models;
using ECommerce.Repositories;

namespace ECommerce.Services;

public class AuthService : IAuthService
{
    private readonly UserRepository _userRepository;

    public AuthService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ResAuthDto> LocalSignin(AuthDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Email))
            throw RequestProblemException.ForMissingField(nameof(dto.Email));

        if (string.IsNullOrWhiteSpace(dto.Password))
            throw RequestProblemException.ForMissingField(nameof(dto.Password));

        User? user = await _userRepository.GetOneByEmail(dto.Email);

        if (user is null)
            throw RequestProblemException.ForInvalidCredentials();

        if (!HashHandler.VerifyPassword(dto.Password, user.Password!, user.Salt!))
            throw RequestProblemException.ForInvalidCredentials();

        return new ResAuthDto("azeazeazeazea", "azeazeddd");
    }
}