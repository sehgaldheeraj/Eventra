using Application.Common.Exceptions;
using Application.Common.Interfaces.Contexts;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Projects.Commands.DeleteProject
{
    public class DeleteProjectCommandHandler(IProjectRepository projectRepository, IUserContext userContext) : IRequestHandler<DeleteProjectCommand, Guid>
    {
        private readonly IProjectRepository _projectRepository = projectRepository;
        private readonly IUserContext _userContext = userContext;

        public async Task<Guid> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException("Project", request.Id);
            project.Delete(_userContext.UserId);
            await _projectRepository.UpdateAsync(project);
            return project.Id;
        }
    }
}
