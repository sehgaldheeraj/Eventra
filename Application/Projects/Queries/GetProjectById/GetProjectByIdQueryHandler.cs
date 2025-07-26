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
    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, Project?>
    {
        public IProjectRepository _repository;
        public GetProjectByIdQueryHandler(IProjectRepository repository) {
            _repository = repository;
        }
        public async Task<Project?> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken) {
            var project = await _repository.GetAsync(request.Id);
            return project;
        }  
    }
}
