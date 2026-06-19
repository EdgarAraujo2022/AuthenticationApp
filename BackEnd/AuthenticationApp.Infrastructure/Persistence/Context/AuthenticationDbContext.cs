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
            entity.ToTable("users", "auth");

            entity.HasKey(u => u.Id);
            entity.Property(u => u.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnType("uuid");

            entity.Property(u => u.Username)
                  .IsRequired()
                  .HasMaxLength(50)
                .HasColumnName("user_name");

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
                   .HasColumnName("password_hash");
            });

            entity.Property(u => u.CreatedAt)
                  .IsRequired()
                  .HasDefaultValueSql("now()")
                  .HasColumnName("created_at");

            // entity.HasIndex(u => u.Email).IsUnique();
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("clients", "auth");

            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id)
              .HasColumnType("uuid")
              .HasDefaultValueSql("gen_random_uuid()");

            entity.Property(c => c.ClientId)
                  .IsRequired()
                  .HasMaxLength(100)
                  .HasColumnName("client_id");

            entity.Property(c => c.ClientSecret)
                  .IsRequired()
                  .HasMaxLength(255)
                  .HasColumnName("client_secret");


            entity.Property(c => c.IsActive)
                  .IsRequired()
                  .HasColumnName("is_active");

            entity.HasIndex(c => c.ClientId).IsUnique();

            entity.HasMany(c => c.ClientScopes)
                .WithOne(cs => cs.Client)
                .HasForeignKey(cs => cs.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(c => c.RefreshTokens)
                  .WithOne(rt => rt.Client)
                  .HasForeignKey(rt => rt.ClientId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ClientScope>(entity =>
        {
            entity.ToTable("client_scopes", "auth");

            entity.HasKey(cs => cs.Id);
            entity.Property(cs => cs.Id)
              .HasDefaultValueSql("gen_random_uuid()")
              .HasColumnType("uuid");

            entity.Property(cs => cs.Scope)
                  .IsRequired()
                  .HasMaxLength(100)
                  .HasColumnName("scope");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
                {
                    entity.HasKey(x => x.Id);

                    entity.Property(x => x.Token)
                        .IsRequired()
                        .HasColumnName("token");

                    entity.Property(x => x.ClientId)
                        .IsRequired()
                        .HasColumnName("client_id");
                });
    }
}