using System;
using ECommerce.Models;

namespace ECommerce.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAll();
    Task<User?> GetOneByEmail(string email);
    Task<User> InsertOne(User user);
}