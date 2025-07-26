using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Projects.Queries.GetProjectById
{
    public class GetProjectByIdQuery : IRequest<Project?>
    {
        public Guid Id;
        public GetProjectByIdQuery(Guid id) { 
            Id = id;
        }
    }
}
