using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Domain.Interfaces;
namespace Application.Sprints.Commands.DeleteSprint
{
    public class DeleteSprintCommandHandler : IRequest<Guid>
    {
        private readonly ISprintRepository _sprintRepository;
        public DeleteSprintCommandHandler(ISprintRepository sprintRepository)
        {
            _sprintRepository = sprintRepository;
        }
        public async Task<Unit> Handle(DeleteSprintCommand command, CancellationToken ct)
        {
            await _sprintRepository.DeleteAsync(command.Id);
            return Unit.Value;
        }
    }
}
