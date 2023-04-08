﻿using System;
using ECommerce.Dto;
using ECommerce.Exceptions;
using ECommerce.Helpers;
using ECommerce.Models;
using ECommerce.Repositories;
using System.Text.RegularExpressions;

namespace ECommerce.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> GetAll() =>
        await _userRepository.GetAll();


    public async Task<User?> GetOneByEmail(string email)
    {
        User? user = await _userRepository.GetOneByEmail(email);

        if (user is null)
            throw RequestProblemException.ForNotFound(email);

        return user;
    }

    public async Task<User?> GetOneByGuid(Guid id)
    {
        User? user = await _userRepository.GetOneByGuid(id);

        if (user is null)
            throw RequestProblemException.ForNotFound(id.ToString());

        return user;
    }

    public async Task<User> InsertOne(UserCreationDto creationDto)
    {
        if (!IsValidEmail(creationDto.Email))
            throw RequestProblemException.ForInvalidField(creationDto.Email);

        User? existingUser = await _userRepository.GetOneByEmail(creationDto.Email);

        if (existingUser is not null)
            throw RequestProblemException.ForConflictingItem(existingUser);

        User user = new()
        {
            Firstname = creationDto.Firstname,
            Lastname = creationDto.Lastname,
            Email = creationDto.Email,
            Password = HashHandler.HashUserPassword(creationDto.Password, out var salt),
            Salt = salt
        };

        return await _userRepository.InsertOne(user);
    }

    private bool IsValidEmail(string email)
    {
        string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov|be|fr)$";

        return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
    }
}