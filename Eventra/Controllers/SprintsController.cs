using Application.Common.Responses;
using Application.Sprints.Commands.CreateSprint;
using Application.Sprints.Commands.DeleteSprint;
using Application.Sprints.Commands.UpdateSprint;
using Application.Sprints.Queries.GetSprintOverview;
using Application.Sprints.Queries.GetSprints;
using Application.Sprints.ReadDtos;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eventra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SprintsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        // GET: api/Sprints
        
        [HttpGet]
        [Obsolete("Audit Resource")]
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
        [HttpGet("{id}/overview")]
        public async Task<ActionResult<ApiResponse<SprintOverview>>> GetSprintOverview(Guid id, CancellationToken ct)
        {
            var sprint = await _mediator.Send(new GetSprintOverviewQuery(id), ct);

            if (sprint == null)
            {
                return NotFound(ApiResponse<SprintOverview>.FailResponse($"Sprint not found"));
            }

            return Ok(ApiResponse<SprintOverview>.SuccessResponse(sprint, "Sprint fetched successfully"));
        }

        // PUT: api/Sprints/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<Guid>>> UpdateSprint(Guid id, [FromBody] UpdateSprintDto dto, CancellationToken ct)
        {
            var command = new UpdateSprintCommand(
                id, dto.Title, dto.Goal, dto.StartDate, dto.EndDate, dto.ProjectId, dto.Status
            );

            var sprintId = await _mediator.Send(command, ct);

            return Ok(ApiResponse<Guid>.SuccessResponse(sprintId, $"Sprint #{sprintId} updated successfully"));
        }

        // POST: api/Sprints
        [HttpPost]
        public async Task<ActionResult<ApiResponse<Guid>>> CreateSprint([FromBody]CreateSprintCommand command, CancellationToken ct)
        {
            var id = await _mediator.Send(command, ct);
            return Ok(ApiResponse<Guid>.SuccessResponse(id, "Sprint created successfully"));
        }

        // DELETE: api/Sprints/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteSprint(Guid id, CancellationToken ct)
        {
            await _mediator.Send(new DeleteSprintCommand(id), ct);
            return Ok(ApiResponse<string>.SuccessMessage("Sprint deleted successfully"));
        }
    }
}
