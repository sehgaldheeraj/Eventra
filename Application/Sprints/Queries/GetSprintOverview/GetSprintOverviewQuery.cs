using Application.Sprints.ReadDtos;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Sprints.Queries.GetSprintOverview
{
    public record GetSprintOverviewQuery(Guid SprintId) : IRequest<SprintOverview>;
}
