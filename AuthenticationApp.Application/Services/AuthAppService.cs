using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthenticationApp.Application.DTOs;
using AuthenticationApp.Application.DTOs.Request;
using AuthenticationApp.Application.Interfaces;
using AuthenticationApp.ApplicationDTOs.Request;
using AuthenticationApp.Domain.Entities;
using AuthenticationApp.Domain.Repositories;
using AuthenticationApp.Domain.Services;
using AuthenticationApp.Domain.ValueObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationApp.Application.Services;

public class AuthAppService : IAuthAppService
{
    private readonly IUserRepository _userRepository;
    private readonly IHashingService _hashingService;
    private readonly IConfiguration _configuration;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IClientRepository _clientRepository;

    public AuthAppService(IUserRepository userRepository,
                          IHashingService hashingService,
                          IConfiguration configuration,
                          IRefreshTokenRepository refreshTokenRepository,
                          IClientRepository clientRepository)
    {
        _userRepository = userRepository;
        _hashingService = hashingService;
        _configuration = configuration;
        _refreshTokenRepository = refreshTokenRepository;
        _clientRepository = clientRepository;
    }

    public async Task<AuthResponseDto> RegisterAsync(UserRegisterRequest dto)
    {
        var client = await _clientRepository.GetByClientIdAsync(dto.ClientId);
        if (client == null || client.ClientSecret != dto.ClientSecret)

            throw new Exception("Client inválido");
        var existing = await _userRepository.GetByEmailAsync(dto.Email);
        if (existing != null)
            throw new Exception("Email já cadastrado");

        var hash = await _hashingService.HashPasswordAsync(dto.Password);
        var password = Password.Create(dto.Password, hash);

        var user = new User(dto.Username, new Email(dto.Email), password);

        await _userRepository.AddAsync(user);

        return await GenerateTokensAsync(user, client);
    }

    public async Task<AuthResponseDto> LoginAsync(UserLoginDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email);
        if (user == null)
            throw new Exception("Usuário ou senha inválidos");

        var isValid = await _hashingService.VerifyPasswordAsync(dto.Password, user.Password.Hash);
        if (!isValid)
            throw new Exception("Usuário ou senha inválidos");

        var client = await _clientRepository.GetByClientIdAsync(dto.ClientId);
        if (client == null || client.ClientSecret != dto.ClientSecret)
            throw new Exception("Client inválido");

        return await GenerateTokensAsync(user, client);
    }
    public async Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequest dto)
    {
        var refreshToken = await _refreshTokenRepository.GetByTokenAsync(dto.RefreshToken);
        if (refreshToken == null)
            throw new Exception("Refresh token inválido");

        if (!refreshToken.IsActive)
            throw new Exception("Refresh token expirado ou revogado");

        var user = await _userRepository.GetByIdAsync(refreshToken.UserId);
        if (user == null)
            throw new Exception("Usuário não encontrado");

        var client = await _clientRepository.GetByClientIdAsync(dto.ClientId);
        if (client == null)
            throw new Exception("Client inválido");

        await _refreshTokenRepository.RevokeAsync(refreshToken);

        return await GenerateTokensAsync(user, client);

    }
    private string GenerateJwtToken(User user, Client client)
    {
        var scopes = string.Join(" ", client.Scopes.Select(s => s.Scope));

        var claims = new[]
        {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email.Value),
        new(ClaimTypes.Role, user.Role ?? string.Empty),
        new("scope", scopes),
        new("client_id", client.ClientId)
    };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!)
        );

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:ExpiryMinutes"]!)),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    private async Task<AuthResponseDto> GenerateTokensAsync(User user, Client client)
    {
        var accessToken = GenerateJwtToken(user, client);

        var refreshTokenValue = Guid.NewGuid().ToString();
        var refreshToken = new RefreshToken(
            user.Id,
            client.Id,
            refreshTokenValue,
            DateTime.UtcNow.AddDays(7)
        );

        await _refreshTokenRepository.AddAsync(refreshToken);

        return new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshTokenValue,
            ExpiresAt = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:ExpiryMinutes"]!)),
            Role = user.Role,
        };
    }
    public async Task LogoutAsync(string refreshToken)
    {
        var storedToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken);

        if (storedToken == null)
            return;

        await _refreshTokenRepository.RevokeAsync(storedToken);
    }
}
