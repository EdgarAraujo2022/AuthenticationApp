
namespace AuthenticationApp.Application.DTOs.Request;

public class UserRegisterRequest
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ClientId { get; set; } = default!;
    public string ClientSecret { get; set; } = default!;
}
