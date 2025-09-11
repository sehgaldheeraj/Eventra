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
        public async Task<ActionResult<ApiResponse<string>>> AssignUserToSprint(Guid issueId, Guid userId)
        {
            await _mediator.Send(new AssignUserCommand(issueId, userId), HttpContext.RequestAborted);

            return ApiResponse<string>.SuccessResponse("Issue assigned to User successfully");
        }

        [HttpDelete("{issueId:guid}/assignee")]
        public async Task<ActionResult<ApiResponse<string>>> UnassignUserFromSprint(Guid issueId)
        {
            await _mediator.Send(new UnassignUserCommand(issueId), HttpContext.RequestAborted);
            return ApiResponse<string>.SuccessResponse("Issue unassigned successfully");
        }
    }
}
