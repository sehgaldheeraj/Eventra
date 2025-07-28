using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using System.Dynamic;
namespace Application.Projects.Commands.UpdateProject
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand>
    {
        private readonly IProjectRepository _projectRepository;
        public UpdateProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task Handle(UpdateProjectCommand request, CancellationToken cancellation)
        {
            var project =await _projectRepository.GetAsync(request.Id) ?? throw new NotFoundException("Project", request.Id);
            project.Name = request.Name;
            project.Description = request.Description;
            await _projectRepository.UpdateAsync(project);
        }
    }
}
