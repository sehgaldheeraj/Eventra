using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Application.Common.Exceptions;
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
        public async  Task<List<Project>> GetAllAsync()
        {
            return await _context.Projects.ToListAsync();
        }
        public async Task<Project?> GetAsync(Guid id)
        {
            var project =  await _context.Projects.FindAsync(id);
            if (project == null) { 
                throw new NotFoundException(nameof(project), id);
            }
            return project;
        }
    }
}
