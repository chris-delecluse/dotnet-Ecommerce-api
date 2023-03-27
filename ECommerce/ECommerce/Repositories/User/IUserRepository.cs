using System;
using ECommerce.Models;

namespace ECommerce.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAll();
    Task<User?> GetOneByEmail(string email);
    Task<User?> GetOneByGuid(Guid id);
    Task<User> InsertOne(User user);
}