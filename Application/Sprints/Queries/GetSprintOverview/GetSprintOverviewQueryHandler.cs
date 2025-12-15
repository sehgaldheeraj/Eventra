using Application.Common.Interfaces;
using Application.Sprints.ReadDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Sprints.Queries.GetSprintOverview
{
    public class GetSprintOverviewQueryHandler(ISprintQueryRepository sprintQueryRepository) : IRequestHandler<GetSprintOverviewQuery, SprintOverview>
    {
        private readonly ISprintQueryRepository _sprintQueryRepository = sprintQueryRepository; 
        public async Task<SprintOverview> Handle(GetSprintOverviewQuery request, CancellationToken cancellationToken)
        {
            var sprintOverview = await _sprintQueryRepository.GetSprintOverviewAsync(request.SprintId, cancellationToken) ?? throw new KeyNotFoundException($"Sprint with ID {request.SprintId} not found.");
            return sprintOverview;
        }
    }
}
