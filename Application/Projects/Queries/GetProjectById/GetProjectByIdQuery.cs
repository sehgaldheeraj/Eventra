using Application.Projects.ReadDtos;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Projects.Queries.GetProjectById
{
    public class GetProjectByIdQuery(Guid id) : IRequest<ProjectOverview?>
    {
        public Guid Id = id;
    }
}
