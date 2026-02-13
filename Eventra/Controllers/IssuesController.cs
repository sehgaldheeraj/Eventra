using Application.Common.Responses;
using Application.IssueActions.Commands.CloseIssue;
using Application.Issues.Commands.CreateIssue;
using Application.Issues.Commands.UpdateIssue;
using Domain.Entities.Issues;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        // PUT: api/Issues/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutIssue(Guid id, UpdateIssueDto updateIssueDto, CancellationToken ct)
        {
            var issueId = await _mediator.Send(new UpdateIssueCommand(id, updateIssueDto.Title, updateIssueDto.Description), ct);

            return Ok(ApiResponse<Guid>.SuccessResponse(issueId, $"Issue #{issueId} updated successfully"));
        }

        //PUT api/Issues/5/close
        [HttpPut("{id:guid}/close")]
        public async Task<IActionResult> CloseIssue(Guid id, CancellationToken ct)
        {
            await _mediator.Send(new CloseIssueCommand(id), ct);

            return Ok(ApiResponse<string>.SuccessResponse("Issue closed successfully"));
        }

        // POST: api/Issues
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Issue>> PostIssue(CreateIssueCommand command, CancellationToken ct)
        {
            var issueId = await _mediator.Send(command, ct);

            var response = ApiResponse<Guid>.SuccessResponse(issueId, $"Issue #{issueId} created successfully");

            return Ok(response);
        }
        //POST: api/Issues/5/SubIssue
        [HttpPost("{id:guid}/SubIssue")]
        public async Task<ActionResult<ApiResponse<string>>> CreateSubIssue(Guid id, CreateIssueCommand command, CancellationToken ct)
        {
            command.ParentIssueId = id;
            var issueId = await _mediator.Send(command, ct);
            var response = ApiResponse<Guid>.SuccessResponse(issueId, $"SubIssue #{issueId} created successfully");

            return Ok(response);
        }
    }
}
