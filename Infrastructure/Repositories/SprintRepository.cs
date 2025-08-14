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
    public class SprintRepository : ISprintRepository
    {
        private readonly EventraDBContext _context;
        public SprintRepository(EventraDBContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Sprint sprint)
        {
            _context.Sprints.Add(sprint);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Sprint sprint)
        {
            _context.Sprints.Update(sprint);
            await _context.SaveChangesAsync();
        }
        public async Task<Sprint?> GetByIdAsync(Guid? Id)
        {
            return await _context.Sprints.Include(s => s.Project).FirstOrDefaultAsync(u => u.Id == Id);
        }    
    }
}
