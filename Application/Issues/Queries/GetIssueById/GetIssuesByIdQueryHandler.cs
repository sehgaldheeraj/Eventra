using Application.Common.Exceptions;
using Application.Common.ResponseDtos;
using Application.Issues.Queries.GetIssues;
using Application.Users.Queries.GetUserById;
using Domain.Entities.Issues;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Issues.Queries.GetIssueById
{
    public class GetIssuesByIdQueryHandler(IIssueRepository issueRepository) : IRequestHandler<GetIssueByIdQuery, IssueDto?>
    {
        private readonly IIssueRepository _issueRepository = issueRepository;

        public async Task<IssueDto?> Handle(GetIssueByIdQuery request, CancellationToken ct)
        {
            var issue = await _issueRepository.GetIssueByIdAsync(request.IssueId, ct) ?? throw new NotFoundException(nameof(Issue), request.IssueId);
            return new IssueDto
            {
                Id = issue.Id,
                Title = issue.Title,
                Description = issue.Description,
                Status = issue.Status.ToString(),
                AssignerId = issue.CreatedById,
                AssigneeId = issue.AssigneeId,
                ProjectId = issue.ProjectId,
                SprintId = issue.SprintId,
                CreatedAt = issue.CreatedAt,
                ClosedAt = issue.ClosedAt
                // If you want extras like AssigneeName → map issue.Assignee?.Name here
            };
        }
    }
}
