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
        public async Task<IEnumerable<Sprint>> GetAllAsync(Guid projectId,
                                string? title,
                                SprintStatus? status,
                                DateTime? from,
                                DateTime? to,
                                CancellationToken cancellationToken
                            )
        {
            var query = _context.Sprints.AsQueryable();

            query = query.Where(s => s.ProjectId == projectId);

            if (status.HasValue)
                query = query.Where(s => s.Status == status.Value);

            if (from.HasValue || to.HasValue)
            {
                if (from.HasValue && to.HasValue)
                {
                    query = query.Where(s =>
                        s.StartDate <= to.Value &&
                        s.EndDate >= from.Value
                    );
                }
                else if (from.HasValue) // only lower bound
                {
                    query = query.Where(s => s.EndDate >= from.Value);
                }
                else if (to.HasValue) // only upper bound
                {
                    query = query.Where(s => s.StartDate <= to.Value);
                }
            }


            if (!string.IsNullOrEmpty(title))
                query = query.Where(s => s.Title.Contains(title));

            return await query.Include(s => s.Project).ToListAsync(cancellationToken);
        }
        public async Task DeleteAsync(Guid id)
        {
            var sprint = new Sprint { Id = id };
            _context.Sprints.Attach(sprint);
            _context.Sprints.Remove(sprint);
            await _context.SaveChangesAsync();
        }
    }
}
