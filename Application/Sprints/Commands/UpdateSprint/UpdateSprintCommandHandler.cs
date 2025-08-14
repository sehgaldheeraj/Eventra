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

            var sprint = await _sprintRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(Sprint), request.Id);

            sprint.ProjectId = request.ProjectId ?? sprint.ProjectId;
            sprint.Title     = request.Title     ?? sprint.Title;
            sprint.Goal      = request.Goal      ?? sprint.Goal;
            sprint.StartDate = request.StartDate ?? sprint.StartDate;
            sprint.EndDate   = request.EndDate   ?? sprint.EndDate;
            sprint.Status    = request.Status    ?? sprint.Status;

            await _sprintRepository.UpdateAsync(sprint);
            return Unit.Value;
        }
    }
}
