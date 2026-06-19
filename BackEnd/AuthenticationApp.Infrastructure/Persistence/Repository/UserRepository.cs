using System;
using System.Threading.Tasks;
using AuthenticationApp.Domain.Entities;
using AuthenticationApp.Domain.Repositories;
using AuthenticationApp.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationApp.Infrastructure.Persistence.Repository;
    public class UserRepository : IUserRepository
    {
        private readonly AuthenticationDbContext _context;

        public UserRepository(AuthenticationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email.Value == email);
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
