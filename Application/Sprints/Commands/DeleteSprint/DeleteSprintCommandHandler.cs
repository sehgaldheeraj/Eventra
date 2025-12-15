using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Domain.Interfaces;
using Application.Common.Exceptions;
using Domain.Entities;
namespace Application.Sprints.Commands.DeleteSprint
{
    public class DeleteSprintCommandHandler(ISprintRepository sprintRepository) : IRequestHandler<DeleteSprintCommand, string>
    {
        private readonly ISprintRepository _sprintRepository = sprintRepository;

        public async Task<string> Handle(DeleteSprintCommand command, CancellationToken ct)
        {
            var sprint = await _sprintRepository.GetSprintByIdAsync(command.Id, ct) ?? throw new NotFoundException(nameof(Sprint), $"Cannot find the sprint #{command.Id}");
            sprint.DeleteSprint();
            await _sprintRepository.DeleteSprintAsync(sprint, ct);
            return $"Sprint #{sprint.Title} has been deleted successfully.";
        }
    }
}
