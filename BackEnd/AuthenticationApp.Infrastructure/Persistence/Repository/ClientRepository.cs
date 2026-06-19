using AuthenticationApp.Domain.Entities;
using AuthenticationApp.Domain.Repositories;
using AuthenticationApp.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationApp.Infrastructure.Persistence.Repository;

public class ClientRepository : IClientRepository
{
    private readonly AuthenticationDbContext _context;

    public ClientRepository(AuthenticationDbContext context)
    {
        _context = context;
    }

    public async Task<Client?> GetByClientIdAsync(string clientId)
    {
        return await _context.Clients
            .Include(c => c.Scopes)
            .FirstOrDefaultAsync(c => c.ClientId == clientId && c.IsActive);
    }

    public async Task AddAsync(Client client)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
    }
}
