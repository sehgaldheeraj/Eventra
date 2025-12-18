using Application.Common.Interfaces;
using Application.Issues.ReadDtos;
using Application.Sprints.ReadDtos;
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
    public class SprintRepository(EventraDBContext context) : ISprintRepository, ISprintQueryRepository
    {
        private readonly EventraDBContext _context = context;

        public async Task AddSprintAsync(Sprint sprint, CancellationToken ct)
        {
            await _context.Sprints.AddAsync(sprint, ct);
            await _context.SaveChangesAsync(ct);
        }
        public async Task UpdateSprintAsync(Sprint sprint, CancellationToken ct)
        {
            _context.Sprints.Update(sprint);
            await _context.SaveChangesAsync(ct);
        }
        public async Task<bool> SprintExists(Guid id, CancellationToken ct)
        {
            return await _context.Sprints.AnyAsync(s => s.Id == id, ct);
        }
        public async Task<SprintOverview?> GetSprintOverviewAsync(Guid id, CancellationToken ct)
        {
            var sprint = await _context.Sprints
        .Where(s => s.Id == id)
        .Select(s => new SprintOverview
        {
            SprintId = s.Id,
            Title = s.Title,
            Goal = s.Goal,
            Status = s.Status,
            StartDate = s.StartDate,
            EndDate = s.EndDate,

            TotalIssues = s.Issues.Count(),
            BacklogCount = s.Issues.Count(i => i.Status == IssueStatus.Backlog),
            ToDoCount = s.Issues.Count(i => i.Status == IssueStatus.ToDo),
            InProgressCount = s.Issues.Count(i => i.Status == IssueStatus.InProgress),
            ClosedCount = s.Issues.Count(i => i.Status == IssueStatus.Closed),

            Issues = s.Issues.Select(i => new IssueSummary
            {
                IssueId = i.Id,
                Title = i.Title,
                AssigneeName = i.Assignee != null ? i.Assignee.Name : null,
                Status = i.Status,
                SubIssueCount = i.SubIssues.Count,
                NoticeCount = 0 // populate if you have messages/notices table
            }).ToList()
        })
        .FirstOrDefaultAsync(ct);

            return sprint;
        }

        public async Task<Sprint?> GetSprintByIdAsync(Guid Id, CancellationToken ct)
        {
            return await _context.Sprints.Include(s => s.Project).FirstOrDefaultAsync(u => u.Id == Id, ct);
        }
        public async Task<IEnumerable<Sprint>> GetAllSprintsAsync(Guid projectId,
                                string? title,
                                SprintStatus? status,
                                DateTime? from,
                                DateTime? to,
                                CancellationToken ct
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

            return await query.Include(s => s.Project).ToListAsync(ct);
        }
        public async Task DeleteSprintAsync(Sprint sprint, CancellationToken ct)
        {
            _context.Sprints.Remove(sprint);
            await _context.SaveChangesAsync(ct);
        }
    }
}
