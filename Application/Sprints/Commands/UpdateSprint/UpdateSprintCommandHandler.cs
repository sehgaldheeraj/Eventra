using Application.Common.Exceptions;
using Application.Common.Interfaces.QueryRepositories;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Sprints.Commands.UpdateSprint
{
    public class UpdateSprintCommandHandler(ISprintRepository sprintRepository, IProjectQueryRepository projectQueryRepository) : IRequestHandler<UpdateSprintCommand, Guid>
    {
        private readonly ISprintRepository _sprintRepository = sprintRepository;
        private readonly IProjectQueryRepository _projectQueryRepository = projectQueryRepository;

        public async Task<Guid> Handle(UpdateSprintCommand request, CancellationToken cancellationToken)
        {

            var sprint = await _sprintRepository.GetSprintByIdAsync(request.Id, cancellationToken) ?? throw new NotFoundException(nameof(Sprint), request.Id);
            if (request.ProjectId.HasValue)
            {
                if (!await _projectQueryRepository.ProjectExistsAsync(request.ProjectId.Value, cancellationToken))
                {
                    throw new NotFoundException("Project", request.ProjectId.Value);
                }
            }
            sprint.UpdateDetails(
                projectId: request.ProjectId,
                title: request.Title,
                goal: request.Goal,
                startDate: request.StartDate,
                endDate: request.EndDate,
                status: request.Status
            );

            await _sprintRepository.UpdateSprintAsync(sprint, cancellationToken);
            return sprint.Id;
        }
    }
}
