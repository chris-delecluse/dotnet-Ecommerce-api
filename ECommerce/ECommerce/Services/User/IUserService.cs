using System;
using ECommerce.Dto;
using ECommerce.Models;

namespace ECommerce.Services;

public interface IUserService
{
    Task<IEnumerable<User>> GetAll();
    Task<User?> GetOneByEmail(string email);
    Task<User?> GetOneByGuid(Guid id);
    Task<User> InsertOne(UserCreationDto creationDto);
}