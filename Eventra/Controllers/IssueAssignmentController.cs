using Application.Common.Responses;
using Application.IssueActions.Commands.AssignUser;
using Application.IssueActions.Commands.UnassignUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eventra.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IssueAssignmentController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPut("{issueId:guid}/assignee/{userId:guid}")]
        public async Task<ActionResult<ApiResponse<Guid>>> AssignUserToSprint(Guid issueId, Guid userId, CancellationToken ct)
        {
            var updatedIssueId = await _mediator.Send(new AssignUserCommand(issueId, userId), ct);

            return Ok(ApiResponse<Guid>.SuccessResponse(updatedIssueId, $"Issue #{updatedIssueId} assigned to User #{userId} successfully."));
        }

        [HttpDelete("{issueId:guid}/assignee")]
        public async Task<ActionResult<ApiResponse<Guid>>> UnassignUserFromSprint(Guid issueId, CancellationToken ct)
        {
            var updatedIssueId = await _mediator.Send(new UnassignUserCommand(issueId), ct);
            return Ok(ApiResponse<Guid>.SuccessResponse(updatedIssueId, $"Issue #{updatedIssueId} has been unassigned successfully."));
        }
    }
}
