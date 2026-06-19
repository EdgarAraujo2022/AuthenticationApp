
using System.Threading.Tasks;
using AuthenticationApp.Application.DTOs;
using AuthenticationApp.Application.DTOs.Request;
using AuthenticationApp.ApplicationDTOs.Request;

namespace AuthenticationApp.Application.Interfaces;

public interface IAuthAppService
{
    Task<AuthResponseDto> RegisterAsync(UserRegisterRequest dto);
    Task<AuthResponseDto> LoginAsync(UserLoginDto dto);
    Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequest dto);
    Task LogoutAsync(string refreshToken);
}

