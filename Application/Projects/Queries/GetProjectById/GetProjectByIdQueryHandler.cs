using Application.Common.Interfaces;
using Application.Projects.ReadDtos;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Projects.Queries.GetProjectById
{
    public class GetProjectByIdQueryHandler(IProjectQueryRepository repository) : IRequestHandler<GetProjectByIdQuery, ProjectOverview?>
    {
        public IProjectQueryRepository _repository = repository;

        public async Task<ProjectOverview?> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken) {
            var projectOverview = await _repository.GetProjectOverviewAsync(request.Id);
            return projectOverview;
        }  
    }
}
