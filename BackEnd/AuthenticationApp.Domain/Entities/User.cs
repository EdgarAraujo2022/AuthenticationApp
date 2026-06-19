using System;
using AuthenticationApp.Domain.ValueObjects;

namespace AuthenticationApp.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public Email Email { get; private set; } = default!;
    public string Username { get; private set; } = string.Empty;
    public Password Password { get; private set; } = default!;
    public DateTime CreatedAt { get; private set; }
    public string Role { get; private set; } = "User";

    private User() { }

    public User(string username, Email email, Password password, string role = "User")
    {
        Id = Guid.NewGuid();
        Username = username ?? throw new ArgumentException("Username é obrigatório.");
        Role = role;
        Email = email ?? throw new ArgumentNullException(nameof(email));
        Password = password ?? throw new ArgumentNullException(nameof(password));
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdatePassword(Password newPassword)
    {
        Password = newPassword;
    }

    public void SetRole(string role)
    {
        Role = role;
    }
}
