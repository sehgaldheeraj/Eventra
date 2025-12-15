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
using Application.Projects.ReadDtos;

namespace Eventra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<ProjectSummary>>>> GetProjectsSummary()
        {
            var projectsSummary = await _mediator.Send(new GetProjectsSummaryQuery());
            return Ok(ApiResponse<IEnumerable<ProjectSummary>>.SuccessResponse(projectsSummary));
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ProjectOverview>>> GetProjectOverview(Guid id)
        {
            var projectOverview = await _mediator.Send(new GetProjectByIdQuery(id));

            if (projectOverview == null)
            {
                return NotFound(ApiResponse<ProjectOverview>.FailResponse("Project not found."));
            }

            return Ok(ApiResponse<ProjectOverview>.SuccessResponse(projectOverview));
        }

        // PUT: api/Projects/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<Guid>>> PutProject(Guid id, [FromBody] UpdateProjectCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest(ApiResponse<string>.FailResponse("ID in URL does not match ID in body."));
            }

            var projectId = await _mediator.Send(command);
            return Ok(ApiResponse<Guid>.SuccessResponse(projectId, "Project updated successfully."));
        }

        // POST: api/Projects
        [HttpPost]
        public async Task<ActionResult<ApiResponse<Guid>>> PostProject([FromBody] CreateProjectCommand command)
        {
            var projectId = await _mediator.Send(command);

            var response = ApiResponse<Guid>.SuccessResponse(projectId, "Project created successfully.");
            return CreatedAtAction(nameof(GetProjectOverview), new { id = projectId }, response);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<Guid>>> DeleteProject(Guid id)
        {
            var deletedProjectId = await _mediator.Send(new DeleteProjectCommand(id));
            return Ok(ApiResponse<Guid>.SuccessResponse(deletedProjectId, $"Project #{deletedProjectId} deleted successfully."));
        }
    }
}
