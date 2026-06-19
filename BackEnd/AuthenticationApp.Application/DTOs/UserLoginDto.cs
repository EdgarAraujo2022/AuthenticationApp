namespace AuthenticationApp.Application.DTOs;

public class UserLoginDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ClientId { get; set; } = default!;
    public string ClientSecret { get; set; } = default!;
}
