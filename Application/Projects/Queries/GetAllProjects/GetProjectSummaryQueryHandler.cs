using Application.Common.Interfaces.QueryRepositories;
using Application.Projects.ReadDtos;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Projects.Queries.GetAllProjects
{
    public class GetProjectSummaryQueryHandler(IProjectQueryRepository repository) : IRequestHandler<GetProjectsSummaryQuery, List<ProjectSummary>>
    {
        private readonly IProjectQueryRepository _repository = repository;

        public async Task<List<ProjectSummary>> Handle(GetProjectsSummaryQuery query, CancellationToken cancellationToken)
        {
            return await _repository.GetProjectsSummaryAsync();
        }
    }
}
