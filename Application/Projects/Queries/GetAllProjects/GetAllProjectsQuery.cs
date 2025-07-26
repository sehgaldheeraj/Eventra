using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Projects.Queries.GetAllProjects
{
    public class GetAllProjectsQuery : IRequest<List<Project>>
    {
    }
}
