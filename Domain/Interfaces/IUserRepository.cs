using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        public Task<Guid> RegisterUserAsync(User user);
        public Task<User?> GetUserByEmailAsync(string email);
        public Task<User?> GetUserByIdAsync(Guid id);
    }
}
