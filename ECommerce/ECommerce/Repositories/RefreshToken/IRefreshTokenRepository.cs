using ECommerce.Models;

namespace ECommerce.Repositories.RefreshToken;

public interface IRefreshTokenRepository
{
    public Task<IEnumerable<Models.RefreshToken>> GetAll();
    public Task<Models.RefreshToken?> GetOneByToken(string tokenValue);
    public Task<IEnumerable<Models.RefreshToken>> GetManyByUser(User user);
    public Task<Models.RefreshToken> InsertOne(Models.RefreshToken refreshToken);
}