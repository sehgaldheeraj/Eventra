using Application.Common.Exceptions;
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
    public class UpdateSprintCommandHandler : IRequestHandler<UpdateSprintCommand, Unit>
    {
        private readonly ISprintRepository _sprintRepository;
        public UpdateSprintCommandHandler(ISprintRepository sprintRepository) { 
            _sprintRepository = sprintRepository;
        }
        public async Task<Unit> Handle(UpdateSprintCommand request, CancellationToken cancellationToken)
        {

            var sprint = await _sprintRepository.GetSprintByIdAsync(request.Id, cancellationToken) ?? throw new NotFoundException(nameof(Sprint), request.Id);

            sprint.UpdateDetails(
                projectId: request.ProjectId,
                title: request.Title,
                goal: request.Goal,
                startDate: request.StartDate,
                endDate: request.EndDate,
                status: request.Status
            );

            await _sprintRepository.UpdateSprintAsync(sprint, cancellationToken);
            return Unit.Value;
        }
    }
}
