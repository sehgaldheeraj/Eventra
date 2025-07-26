using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProjectRepository: IProjectRepository
    {
        private readonly EventraDBContext _context;
        public ProjectRepository(EventraDBContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
        } 
    }
}
