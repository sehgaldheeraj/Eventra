using Application.Common.Responses;
using Application.Sprints.Commands.CreateSprint;
using Application.Sprints.Commands.DeleteSprint;
using Application.Sprints.Commands.UpdateSprint;
using Application.Sprints.Queries.GetSprints;
using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Eventra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SprintsController : ControllerBase
    {
        private readonly EventraDBContext _context;
        private readonly IMediator _mediator;

        public SprintsController(EventraDBContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        // GET: api/Sprints
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<Sprint>>>> GetSprints([FromQuery] GetSprintsQuery query, CancellationToken ct)
        {
            if (query.ProjectId == Guid.Empty)
                return BadRequest(ApiResponse<IEnumerable<Sprint>>.FailResponse("ProjectId is required"));
            var sprints = await _mediator.Send(query, ct);

            if (sprints == null || !sprints.Any())
            {
                return NotFound(ApiResponse<IEnumerable<Sprint>>.FailResponse("No sprints found"));
            }

            return Ok(ApiResponse<IEnumerable<Sprint>>.SuccessResponse(sprints, "Sprints fetched successfully"));
        }

        // GET: api/Sprints/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Sprint>>> GetSprint(Guid id)
        {
            var sprint = await _context.Sprints.Include(s => s.Project).FirstOrDefaultAsync(u => u.Id == id);

            if (sprint == null)
            {
                return NotFound(ApiResponse<Sprint>.FailResponse("Sprint not found"));
            }

            return Ok(ApiResponse<Sprint>.SuccessResponse(sprint, "Sprint fetched successfully"));
        }

        // PATCH: api/Sprints/5
        [HttpPatch("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> PatchSprint(Guid id, [FromBody] UpdateSprintDto dto)
        {
            var command = new UpdateSprintCommand(
                id, dto.Title, dto.Goal, dto.StartDate, dto.EndDate, dto.ProjectId, dto.Status
            );

            await _mediator.Send(command);

            return Ok(ApiResponse<string>.SuccessMessage("Sprint updated successfully"));
        }

        // POST: api/Sprints
        [HttpPost]
        public async Task<ActionResult<ApiResponse<Guid>>> PostSprint([FromBody]CreateSprintCommand command)
        {
            var id = await _mediator.Send(command);
            var response = ApiResponse<Guid>.SuccessResponse(id, "Sprint created successfully");

            return CreatedAtAction("GetSprint", new { id }, response);
        }

        // DELETE: api/Sprints/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteSprint(Guid id, DeleteSprintCommand command)
        {
            //var command = 
            await _mediator.Send(command);

            return Ok(ApiResponse<string>.SuccessMessage("Sprint deleted successfully"));
        }

        private bool SprintExists(Guid id)
        {
            return _context.Sprints.Any(e => e.Id == id);
        }
    }
}
