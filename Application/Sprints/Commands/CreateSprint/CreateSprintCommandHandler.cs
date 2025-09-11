using Application.Common.Exceptions;
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
    public class CreateSprintCommandHandler(ISprintRepository sprintRepository, IProjectRepository projectRepository) : IRequestHandler<CreateSprintCommand, Guid>
    {
        private readonly ISprintRepository _sprintRepository = sprintRepository;
        private readonly IProjectRepository _projectRepository = projectRepository;

        public async Task<Guid> Handle(CreateSprintCommand request, CancellationToken cancellationToken)
        {
            Project project = await _projectRepository.GetAsync(request.ProjectId) ?? throw new NotFoundException(nameof(Project), request.ProjectId);

            var sprint = new Sprint(request.Title, request.Goal ?? string.Empty, request.StartDate, request.EndDate, request.ProjectId, project);
            await _sprintRepository.AddSprintAsync(sprint, cancellationToken);
            return sprint.Id;
        }
    }
}
