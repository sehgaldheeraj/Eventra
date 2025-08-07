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
    public class UserRepository : IUserRepository
    {
        private readonly EventraDBContext _dbContext;
        public UserRepository(EventraDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Guid> RegisterUserAsync(User user) {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return user.Id;
        }
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }
        public async Task<User?> GetUserByIdAsync(Guid Id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == Id);
        }

    }
}
