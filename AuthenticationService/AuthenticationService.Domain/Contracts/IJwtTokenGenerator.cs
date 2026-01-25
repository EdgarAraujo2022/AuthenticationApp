using AuthenticationService.Domain.Entities;

namespace AuthenticationService.Domain.Contracts;

public interface IJwtTokenGenerator
{
    string Generate(User user);
}