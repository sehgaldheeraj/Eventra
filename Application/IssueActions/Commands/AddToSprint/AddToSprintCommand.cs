using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.ResponseDtos;
using Domain.Entities;
using MediatR;
namespace Application.IssueActions.Commands.AddToSprint
{
    public record AddToSprintCommand(Guid SprintId, Guid IssueId) : IRequest<Guid>;
}
