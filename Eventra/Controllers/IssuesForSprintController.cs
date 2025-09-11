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
    public class IssuesForSprintController : ControllerBase
    {
        private readonly IMediator _mediator;
        public IssuesForSprintController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("{issueId:guid}/sprint/{sprintId:guid}")]
        public async Task<ActionResult<ApiResponse<string>>> AddIssueToSprint(Guid sprintId, Guid issueId)
        {

            var command = new AddToSprintCommand(sprintId, issueId);
            await _mediator.Send(command, HttpContext.RequestAborted);
            return ApiResponse<string>.SuccessMessage("Issue added to Sprint successfully");
        }

        [HttpDelete("{issueId:guid}/sprint")]
        public async Task<ActionResult<ApiResponse<string>>> RemoveIssueFromSprint(Guid issueId)
        {
            var command = new RemoveFromSprintCommand(issueId);
            await _mediator.Send(command, HttpContext.RequestAborted);
            return ApiResponse<string>.SuccessMessage("Issue Removed from Sprint successfully");
        }
    }
}