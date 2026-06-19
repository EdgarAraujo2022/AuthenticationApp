using System.Threading.Tasks;

namespace AuthenticationApp.Domain.Services;

public interface IHashingService
{
    Task<string> HashPasswordAsync(string password);
    Task<bool> VerifyPasswordAsync(string password, string hash);
}
