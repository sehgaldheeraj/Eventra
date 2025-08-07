using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Projects.Commands.CreateProject;
using Application.Projects.Commands.DeleteProject;
using Application.Projects.Commands.UpdateProject;
using Application.Projects.Queries.GetAllProjects;
using Application.Projects.Queries.GetProjectById;
using Application.Common.Responses;
using Domain.Entities;

namespace Eventra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<Project>>>> GetProjects()
        {
            var projects = await _mediator.Send(new GetAllProjectsQuery());
            return Ok(ApiResponse<IEnumerable<Project>>.SuccessResponse(projects));
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Project>>> GetProject(Guid id)
        {
            var project = await _mediator.Send(new GetProjectByIdQuery(id));

            if (project == null)
            {
                return NotFound(ApiResponse<Project>.FailResponse("Project not found."));
            }

            return Ok(ApiResponse<Project>.SuccessResponse(project));
        }

        // PUT: api/Projects/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> PutProject(Guid id, [FromBody] UpdateProjectCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest(ApiResponse<string>.FailResponse("ID in URL does not match ID in body."));
            }

            await _mediator.Send(command);
            return Ok(ApiResponse<string>.SuccessMessage("Project updated successfully."));
        }

        // POST: api/Projects
        [HttpPost]
        public async Task<ActionResult<ApiResponse<Guid>>> PostProject([FromBody] CreateProjectCommand command)
        {
            var projectId = await _mediator.Send(command);

            var response = ApiResponse<Guid>.SuccessResponse(projectId, "Project created successfully.");
            return CreatedAtAction(nameof(GetProject), new { id = projectId }, response);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteProject(Guid id)
        {
            await _mediator.Send(new DeleteProjectCommand(id));
            return Ok(ApiResponse<string>.SuccessMessage("Project deleted successfully."));
        }
    }
}
