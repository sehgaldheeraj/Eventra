using Application.Common.Interfaces.QueryRepositories;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository(EventraDBContext dbContext) : IUserRepository, IUserQueryRepository
    {
        private readonly EventraDBContext _dbContext = dbContext;

        public async Task<Guid> RegisterUserAsync(User user) {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return user.Id;
        }
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }
        public async Task<User?> GetUserByIdAsync(Guid? Id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == Id);
        }
        public async Task<bool> UserExistsAsync(Guid userId, CancellationToken ct)
        {
            return await _dbContext.Users.AnyAsync(u => u.Id == userId, ct);
        }
    }
}
