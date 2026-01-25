namespace AuthenticationService.Domain.Entities;

public class User
{
    public Guid Id { get; init; }
    public string Email { get; init; } = default!;
    public string Role { get; init; } = default!;
}