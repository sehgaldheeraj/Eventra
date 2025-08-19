using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Sprints.Commands.DeleteSprint
{
    public record DeleteSprintCommand(Guid Id) : IRequest;
}
