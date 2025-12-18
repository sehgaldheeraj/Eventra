using Application.Common.Responses;
using Application.IssueActions.Commands.AddToSprint;
using Application.IssueActions.Commands.RemoveFromSprint;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eventra.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IssuesForSprintController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPut("{issueId:guid}/sprint/{sprintId:guid}")]
        public async Task<ActionResult<ApiResponse<Guid>>> AddIssueToSprint(Guid sprintId, Guid issueId, CancellationToken ct)
        {
            var command = new AddToSprintCommand(sprintId, issueId);
            var updatedIssueId = await _mediator.Send(command, ct);
            return Ok(ApiResponse<Guid>.SuccessResponse(updatedIssueId, $"Issue #{updatedIssueId} added to Sprint #{sprintId} successfully"));
        }

        [HttpDelete("{issueId:guid}/sprint")]
        public async Task<ActionResult<ApiResponse<Guid>>> RemoveIssueFromSprint(Guid issueId, CancellationToken ct)
        {
            var command = new RemoveFromSprintCommand(issueId);
            var updatedIssueId = await _mediator.Send(command, ct);
            return Ok(ApiResponse<Guid>.SuccessResponse(updatedIssueId, $"Issue #{updatedIssueId} Removed from Sprint successfully"));
        }
    }
}