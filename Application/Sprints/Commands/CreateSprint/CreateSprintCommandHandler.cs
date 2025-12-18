using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Projects.ReadDtos;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Sprints.Commands.CreateSprint
{
    public class CreateSprintCommandHandler(ISprintRepository sprintRepository, IProjectQueryRepository projectRepository) : IRequestHandler<CreateSprintCommand, Guid>
    {
        private readonly ISprintRepository _sprintRepository = sprintRepository;
        private readonly IProjectQueryRepository _projectRepository = projectRepository;

        public async Task<Guid> Handle(CreateSprintCommand request, CancellationToken cancellationToken)
        {
            if(!await _projectRepository.ProjectExistsAsync(request.ProjectId, cancellationToken))
            {
                throw new NotFoundException("Project", request.ProjectId);
            }

            var sprint = new Sprint(request.Title, request.Goal ?? string.Empty, request.StartDate, request.EndDate, request.ProjectId);
            await _sprintRepository.AddSprintAsync(sprint, cancellationToken);
            return sprint.Id;
        }
    }
}
