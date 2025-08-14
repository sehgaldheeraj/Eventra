using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Sprints.Commands.CreateSprint
{
    public class CreateSprintCommandHandler : IRequestHandler<CreateSprintCommand, Guid>
    {
        private readonly ISprintRepository _sprintRepository;
        public CreateSprintCommandHandler(ISprintRepository sprintRepository)
        {
            _sprintRepository = sprintRepository;
        }
        public async Task<Guid> Handle(CreateSprintCommand request, CancellationToken cancellationToken)
        {
            var sprint = new Sprint
            {
                Id = Guid.NewGuid(),
                ProjectId = request.ProjectId,
                Title = request.Title,
                Goal = request.Goal,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Status = SprintStatus.Planned
            };
            await _sprintRepository.AddAsync(sprint);
            return sprint.Id;
        }
    }
}
