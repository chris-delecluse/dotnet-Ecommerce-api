using ECommerce.DataAccess;
using ECommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories;

public class TokenRepository : ITokenRepository
{
    private readonly DatabaseContext _dbContext;

    public TokenRepository(DatabaseContext dbContext) => _dbContext = dbContext;

    public async Task<IEnumerable<RefreshToken>> GetAll() =>
        await _dbContext.RefreshTokens.ToListAsync();

    public Task<RefreshToken?> GetOneByToken(string tokenValue) =>
        _dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == tokenValue);

    public async Task<IEnumerable<RefreshToken>> GetManyByUser(User user) =>
        await _dbContext.RefreshTokens.Where(rt => rt.User == user).ToListAsync();
    
    public async Task<RefreshToken> InsertOne(RefreshToken refreshToken)
    {
        await _dbContext.RefreshTokens.AddAsync(refreshToken);
        await _dbContext.SaveChangesAsync();

        return refreshToken;
    }
}