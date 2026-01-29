using AuthenticationApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationApp.Infrastructure.Persistence.Context;

public class AuthenticationDbContext : DbContext
{
    public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<ClientScope> ClientScopes => Set<ClientScope>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users", "auth");

            entity.HasKey(u => u.Id);
            entity.Property(u => u.Id)
              .HasColumnType("uuid");

            entity.Property(u => u.Username)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.OwnsOne(u => u.Email, email =>
            {
                email.Property(e => e.Value)
                     .IsRequired()
                     .HasMaxLength(100)
                     .HasColumnName("Email");
            });

            entity.OwnsOne(u => u.Password, pwd =>
            {
                pwd.Property(p => p.Hash)
                   .IsRequired()
                   .HasColumnName("PasswordHash");
            });

            entity.Property(u => u.CreatedAt)
                  .IsRequired();
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("Clients", "auth");

            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id)
              .HasColumnType("uuid");

            entity.Property(c => c.ClientId)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(c => c.ClientSecret)
                  .IsRequired()
                  .HasMaxLength(255);

            entity.Property(c => c.IsActive)
                  .IsRequired();
        });

        modelBuilder.Entity<ClientScope>(entity =>
        {
            entity.ToTable("ClientScopes", "auth");

            entity.HasKey(cs => cs.Id);
            entity.Property(cs => cs.Id)
              .HasColumnType("uuid");

            entity.Property(cs => cs.Scope)
                  .IsRequired()
                  .HasMaxLength(100);
        });
        
        modelBuilder.Entity<RefreshToken>(entity =>
                {
                    entity.HasKey(x => x.Id);

                    entity.Property(x => x.Token)
                        .IsRequired();

                    entity.Property(x => x.ClientId)
                        .IsRequired();
                });
    }
}