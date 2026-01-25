using AuthenticationService.Services.Dtos;
namespace AuthenticationService.Services.Interfaces
{
    public interface IAuthService
    {
        LoginResponseDto Login(LoginRequestDto request);
        UserMeDto GetMe(Guid userId, string email, string role);
    }
}