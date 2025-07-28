using Application.Common.Exceptions;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Projects.Commands.DeleteProject
{
    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand>
    {
        private readonly IProjectRepository _repository;
        public DeleteProjectCommandHandler(IProjectRepository repository) { 
            _repository = repository;
        }
        public async Task Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetAsync(request.Id) ?? throw new NotFoundException("Project", request.Id);
            await _repository.DeleteAsync(project);

        }
    }
}
