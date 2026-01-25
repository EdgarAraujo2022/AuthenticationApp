using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Services.Interfaces;
using AuthenticationService.Services.Dtos;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequestDto request)
        => Ok(_authService.Login(request));

    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        var userId = Guid.Parse(User.FindFirstValue(JwtRegisteredClaimNames.Sub)!);
        var email = User.FindFirstValue(JwtRegisteredClaimNames.Email)!;
        var role = User.FindFirstValue(ClaimTypes.Role)!;

        return Ok(_authService.GetMe(userId, email, role));
    }
}
