using AuthenticationApp.Domain.Entities;

namespace AuthenticationApp.Domain.Repositories;

public interface IClientRepository
{
    Task<Client?> GetByClientIdAsync(string clientId);
    Task AddAsync(Client client);
}
