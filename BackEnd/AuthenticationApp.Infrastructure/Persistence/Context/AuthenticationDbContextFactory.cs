using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using AuthenticationApp.Infrastructure.Persistence.Context;

namespace AuthenticationApp.Infrastructure.Persistence
{
    public class AuthenticationDbContextFactory : IDesignTimeDbContextFactory<AuthenticationDbContext>
    {
        public AuthenticationDbContext CreateDbContext(string[] args)
        {
            // Carrega appsettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<AuthenticationDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new AuthenticationDbContext(optionsBuilder.Options);
        }
    }
}
