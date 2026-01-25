using AuthenticationService.Services.Dtos;
using AuthenticationService.Services.Interfaces;
using AuthenticationService.Domain.Contracts;
using AuthenticationService.Domain.Entities;

namespace AuthenticationService.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IJwtTokenGenerator _tokenGenerator;

        public AuthService(IJwtTokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
        }

        public LoginResponseDto Login(LoginRequestDto request)
        {
            // Fake login (por enquanto)
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                Role = "Admin"
            };

            var token = _tokenGenerator.Generate(user);

            return new LoginResponseDto(token);
        }

        public UserMeDto GetMe(Guid userId, string email, string role)
            => new(userId, email, role);
    }
}