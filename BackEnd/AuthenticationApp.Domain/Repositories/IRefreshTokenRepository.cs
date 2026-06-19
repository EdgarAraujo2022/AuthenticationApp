using System.Threading.Tasks;
using AuthenticationApp.Domain.Entities;

namespace AuthenticationApp.Domain.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken token);
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task RevokeAsync(RefreshToken token);
        Task<RefreshToken?> GetAsync(string token, Guid clientId);
    }
}
