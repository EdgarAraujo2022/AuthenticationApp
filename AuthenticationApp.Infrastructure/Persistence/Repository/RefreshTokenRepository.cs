using System.Threading.Tasks;
using AuthenticationApp.Domain.Entities;
using AuthenticationApp.Domain.Repositories;
using AuthenticationApp.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationApp.Infrastructure.Persistence.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AuthenticationDbContext _context;

        public RefreshTokenRepository(AuthenticationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(RefreshToken token)
        {
            await _context.RefreshTokens.AddAsync(token);
            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _context.RefreshTokens
                .FirstOrDefaultAsync(x => x.Token == token);
        }

        public async Task RevokeAsync(RefreshToken token)
        {
            token.Revoke();
            _context.RefreshTokens.Update(token);
            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken?> GetAsync(string token, Guid clientId)
        {
            return await _context.RefreshTokens
                .FirstOrDefaultAsync(rt =>
                    rt.Token == token &&
                    rt.ClientId == clientId);
        }


    }
}
