using Application.Common.Exceptions;
using Application.Common.Interfaces.QueryRepositories;
using Application.Projects.ReadDtos;
using Application.Sprints.ReadDtos;
using Domain.Entities;
using Domain.Entities.Projects;
using Domain.Interfaces;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProjectRepository(EventraDBContext context) : IProjectRepository, IProjectQueryRepository
    {
        private readonly EventraDBContext _context = context;

        public async Task AddAsync(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
        }
        public async  Task<List<ProjectSummary>> GetProjectsSummaryAsync()
        {
            var result = 
                from p in _context.Projects
                join u in _context.Users on p.OwnerId equals u.Id
                select new ProjectSummary
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    OwnerName = u.Name,
                    OwnerRole = u.Role
                };
            return await result.ToListAsync();
        }
        public async Task<ProjectOverview?> GetProjectOverviewAsync(Guid projectId)
        {
            return await _context.Projects
            .Where(p => p.Id == projectId)
            .Select(p => new ProjectOverview
            {
                ProjectId = p.Id,
                Name = p.Name,
                Description = p.Description,
                CreatedAt = p.CreatedAt,

                OwnerName = p.Owner.Name,
                OwnerRole = p.Owner.Role,

                TotalIssues = p.Issues.Count(),
                OpenIssues = p.Issues.Count(i =>
                    i.Status == IssueStatus.Backlog ||
                    i.Status == IssueStatus.ToDo ||
                    i.Status == IssueStatus.InProgress
                ),

                TotalSprints = p.Sprints.Count(),

                // First active; if none then first planned; else null
                OpenSprintId = p.Sprints
                    .Where(s => s.Status == SprintStatus.Active)
                    .Select(s => (Guid?)s.Id)
                    .FirstOrDefault()
                    ??
                    p.Sprints
                    .Where(s => s.Status == SprintStatus.Planned)
                    .Select(s => (Guid?)s.Id)
                    .FirstOrDefault(),

                HasOpenSprint = p.Sprints
                    .Any(s => s.Status == SprintStatus.Active || s.Status == SprintStatus.Planned),

                Sprints = p.Sprints
                    .OrderBy(s => s.StartDate)
                    .Select(s => new SprintTimeline
                    {
                        SprintId = s.Id,
                        Title = s.Title,
                        StartDate = s.StartDate,
                        EndDate = s.EndDate,
                        Status = s.Status,
                        IssueCount = s.Issues.Count()
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync();
        }
        public async Task<bool> ProjectExistsAsync(Guid id, CancellationToken ct)
        {
            return await _context.Projects.AnyAsync(p => p.Id == id, ct);
        }
        public async Task<Project?> GetByIdAsync(Guid id)
        {
            return await _context.Projects.FindAsync(id);
        }   
        public async Task UpdateAsync(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
            
        }
    }
}
