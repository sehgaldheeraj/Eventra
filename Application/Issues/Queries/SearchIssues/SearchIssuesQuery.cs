using Application.Common.ResponseDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Issues.Queries.SearchIssues
{
    public record SearchIssuesQuery(Guid ProjectId, string Keyword) : IRequest<IEnumerable<IssueDto>>;
}
