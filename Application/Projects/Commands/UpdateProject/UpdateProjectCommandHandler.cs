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
    public class UpdateProjectCommandHandler(IProjectRepository projectRepository) : IRequestHandler<UpdateProjectCommand, Guid>
    {
        private readonly IProjectRepository _projectRepository = projectRepository;

        public async Task<Guid> Handle(UpdateProjectCommand request, CancellationToken cancellation)
        {
            var project =await _projectRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException("Project", request.Id);

            project.UpdateDetails(
                name: request.Name,
                description: request.Description
            );

            if (request.OwnerId.HasValue)
            {
                project.ChangeOwner(request.OwnerId.Value);
            }

            await _projectRepository.UpdateAsync(project);

            return project.Id;
        }
    }
}
