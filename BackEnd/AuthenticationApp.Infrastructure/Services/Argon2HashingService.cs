using System;
using System.Threading.Tasks;
using AuthenticationApp.Domain.Services;
using Isopoh.Cryptography.Argon2;

namespace AuthenticationApp.Infrastructure.Services;

public class Argon2HashingService : IHashingService
{
    public Task<string> HashPasswordAsync(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Senha não pode ser vazia.");

        // Cria o hash usando o Isopoh.Argon2
        string hash = Argon2.Hash(password);
        return Task.FromResult(hash);
    }

    public Task<bool> VerifyPasswordAsync(string password, string hash)
    {
        if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(hash))
            return Task.FromResult(false);

        bool result = Argon2.Verify(hash, password);
        return Task.FromResult(result);
    }
}
