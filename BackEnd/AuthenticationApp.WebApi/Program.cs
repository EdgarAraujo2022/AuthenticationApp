using AuthenticationApp.Infrastructure.Persistence;
using AuthenticationApp.Infrastructure.Services;
using AuthenticationApp.Domain.Services;
using Microsoft.EntityFrameworkCore;
using AuthenticationApp.Infrastructure.Persistence.Context;
using AuthenticationApp.Domain.Repositories;
using AuthenticationApp.Infrastructure.Persistence.Repository;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using AuthenticationApp.Application.Services;
using AuthenticationApp.Application.Interfaces;
using AuthenticationApp.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAuthAppService, AuthAppService>();
builder.Services.AddScoped<IHashingService, Argon2HashingService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();

var jwtSecret = builder.Configuration["Jwt:Secret"]!;
var key = Encoding.ASCII.GetBytes(jwtSecret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = System.TimeSpan.Zero
    };
});

builder.Services.AddControllers();

// CONFIGURAÇÃO DO CORS - ADICIONE ESTA PARTE
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        corsBuilder =>
        {
            corsBuilder.WithOrigins("http://localhost:4200", "http://localhost:3000")
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials();
        });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AuthenticationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IHashingService, Argon2HashingService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// USE CORS - ADICIONE ESTA LINHA ANTES DO UseAuthorization
app.UseCors("AllowAngularApp");

app.UseAuthorization();

app.MapControllers();

app.Run();