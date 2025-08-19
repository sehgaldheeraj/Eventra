using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Sprints.Queries.GetSprints
{
    public class GetSprintsQueryHandler : IRequestHandler<GetSprintsQuery, IEnumerable<Sprint>>
    {
        private readonly ISprintRepository _sprintRepository;
        public GetSprintsQueryHandler(ISprintRepository sprintRepository)
        {
            _sprintRepository = sprintRepository;
        }
        public async Task<IEnumerable<Sprint>> Handle(GetSprintsQuery query, CancellationToken cancellationToken)
        {
            
            return await _sprintRepository.GetAllAsync(
                query.ProjectId,
                query.Title,
                query.Status,
                query.From,
                query.To,
                cancellationToken
            );
        }
    }
}
