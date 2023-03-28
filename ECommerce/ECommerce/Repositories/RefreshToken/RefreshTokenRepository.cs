using ECommerce.DataAccess;
using ECommerce.Models;

namespace ECommerce.Repositories.RefreshToken;

public class RefreshTokenRepository: IRefreshTokenRepository
{
    private readonly DatabaseContext _dbContext;

    public RefreshTokenRepository(DatabaseContext dbContext) => _dbContext = dbContext;

    public Task<IEnumerable<Models.RefreshToken>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Models.RefreshToken?> GetOneByToken(string tokenValue)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Models.RefreshToken>> GetManyByUser(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<Models.RefreshToken> InsertOne(Models.RefreshToken refreshToken)
    {
        await _dbContext.RefreshTokens.AddAsync(refreshToken);
        await _dbContext.SaveChangesAsync();
        
        return refreshToken;
    }
}