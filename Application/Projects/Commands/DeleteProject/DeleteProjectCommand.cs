using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace Application.Projects.Commands.DeleteProject
{
    public class DeleteProjectCommand : IRequest
    {
        public Guid Id { get; set; }
        public DeleteProjectCommand(Guid id)
        {
            Id = id;
        }
    }
}
