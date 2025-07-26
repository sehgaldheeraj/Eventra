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
    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, List<Project>>
    {
        private readonly IProjectRepository _repository;
        public GetAllProjectsQueryHandler(IProjectRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<Project>> Handle(GetAllProjectsQuery query, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
