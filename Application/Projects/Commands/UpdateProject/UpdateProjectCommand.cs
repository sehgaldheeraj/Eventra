using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Projects.Commands.UpdateProject
{
    public class UpdateProjectCommand : IRequest
    {
        public Guid Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
