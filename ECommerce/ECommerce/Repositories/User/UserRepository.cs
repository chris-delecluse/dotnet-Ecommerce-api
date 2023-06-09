﻿using System;
using ECommerce.DataAccess;
using ECommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _dbContext;

    public UserRepository(DatabaseContext dbContext) => _dbContext = dbContext;

    public async Task<IEnumerable<User>> GetAll() =>
        await _dbContext.User.ToListAsync();

    public async Task<User?> GetOneByEmail(string email) =>
        await _dbContext.User.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<User?> GetOneByGuid(Guid id) =>
        await _dbContext.User.FirstOrDefaultAsync(u => u.Id == id);

    public async Task<User> InsertOne(User user)
    {
        await _dbContext.User.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        return user;
    }
}