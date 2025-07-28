using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Application.Projects.Commands.CreateProject;
using Application.Projects.Queries.GetAllProjects;
using Application.Projects.Queries.GetProjectById;
using Application.Projects.Commands.UpdateProject;
using Application.Projects.Commands.DeleteProject;

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
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            var projects = await _mediator.Send(new GetAllProjectsQuery());
            return Ok(projects);
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Project?>> GetProject(Guid id)
        {
            var project = await _mediator.Send(new GetProjectByIdQuery(id));
            return project;
        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(Guid id,[FromBody] UpdateProjectCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            await _mediator.Send(command);
            return NoContent();
        }

        // POST: api/Projects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject([FromBody] CreateProjectCommand command)
        {
            var projectId = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetProject), new { id = projectId }, projectId);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            await _mediator.Send(new DeleteProjectCommand(id));
            return NoContent();
        }
    }
}
