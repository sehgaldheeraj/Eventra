using Application.Common.ResponseDtos;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Issues.Queries.GetIssues
{
    public class GetIssuesQueryHandler(IIssueRepository issueRepository) : IRequestHandler<GetIssuesQuery, IEnumerable<IssueDto?>>
    {
        private readonly IIssueRepository _issueRepository = issueRepository;

        public async Task<IEnumerable<IssueDto?>> Handle(GetIssuesQuery request, CancellationToken ct)
        {
            var issues = await _issueRepository.GetIssuesAsync(
            request.ParentIssueId,
            request.SprintId,
            request.UserId,
            request.ProjectId,
            ct
            );

            return issues.Select(issue => new IssueDto
            {
                Id = issue.Id,
                Title = issue.Title,
                Description = issue.Description,
                Status = issue.Status.ToString(),
                AssignerId = issue.CreatedById,
                AssigneeId = issue.AssigneeId,
                ProjectId = issue.ProjectId,
                SprintId = issue.SprintId,
                ParentIssueId = issue.ParentIssueId,
                CreatedAt = issue.CreatedAt,
                ClosedAt = issue.ClosedAt
            });
        }
    }
}
