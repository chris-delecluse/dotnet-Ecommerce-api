using ECommerce.Models;

namespace ECommerce.Repositories;

public interface ITokenRepository
{
    public Task<IEnumerable<RefreshToken>> GetAll();
    public Task<RefreshToken?> GetOneByToken(string tokenValue);
    public Task<IEnumerable<RefreshToken>> GetManyByUser(User user);
    public Task<RefreshToken> InsertOne(RefreshToken refreshToken);
}