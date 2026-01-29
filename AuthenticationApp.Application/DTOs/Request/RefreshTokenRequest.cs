
namespace AuthenticationApp.ApplicationDTOs.Request;

public class RefreshTokenRequest
{
    public string RefreshToken { get; set; } = string.Empty;
    public string ClientId { get; set; } = default!;
}
