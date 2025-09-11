using Application.Common.ResponseDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Issues.Queries.GetIssues
{
    public record GetIssuesQuery(
            Guid? ParentIssueId = null,
            Guid? SprintId = null,
            Guid? UserId = null,
            Guid? ProjectId = null
            ) : IRequest<IEnumerable<IssueDto>>;
}
