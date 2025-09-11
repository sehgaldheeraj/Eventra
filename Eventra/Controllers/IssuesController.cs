using Application.Common.Responses;
using Application.IssueActions.Commands.CloseIssue;
using Application.Issues.Commands.CreateIssue;
using Application.Issues.Commands.UpdateIssue;
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

namespace Eventra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuesController : ControllerBase
    {
        private readonly EventraDBContext _context;
        private readonly IMediator _mediator;

        public IssuesController(EventraDBContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        // GET: api/Issues
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Issue>>> GetIssues()
        {
            return await _context.Issues.ToListAsync();
        }

        // GET: api/Issues/5
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Issue>> GetIssue(Guid id)
        {
            var issue = await _context.Issues.FindAsync(id);

            if (issue == null)
            {
                return NotFound();
            }

            return issue;
        }

        // PUT: api/Issues/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutIssue(Guid id, UpdateIssueDto updateIssueDto)
        {
            await _mediator.Send(new UpdateIssueCommand(id, updateIssueDto.Title, updateIssueDto.Description));

            return Ok(ApiResponse<string>.SuccessResponse("Issue updated successfully"));
        }

        //PUT api/Issues/5/close
        [HttpPut("{id:guid}/close")]
        public async Task<IActionResult> CloseIssue(Guid id)
        {
            await _mediator.Send(new CloseIssueCommand(id));

            return Ok(ApiResponse<string>.SuccessResponse("Issue closed successfully"));
        }

        // POST: api/Issues
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Issue>> PostIssue(CreateIssueCommand command)
        {
            var issueId = await _mediator.Send(command);

            var response = ApiResponse<Guid>.SuccessResponse(issueId, "Issue created successfully");

            return CreatedAtAction(nameof(GetIssues), new { issueId }, response);
        }
        //POST: api/Issues/5/SubIssue
        [HttpPost("{id:guid}/SubIssue")]
        public async Task<ActionResult<ApiResponse<string>>> CreateSubIssue(Guid id, CreateIssueCommand command)
        {
            command.ParentIssueId = id;
            var issueId = await _mediator.Send(command, HttpContext.RequestAborted);
            var response = ApiResponse<Guid>.SuccessResponse(issueId, "SubIssue created successfully");

            return CreatedAtAction(nameof(GetIssues), new { issueId }, response);
        }
        // DELETE: api/Issues/5
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteIssue(Guid id)
        {
            var issue = await _context.Issues.FindAsync(id);
            if (issue == null)
            {
                return NotFound();
            }

            _context.Issues.Remove(issue);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IssueExists(Guid id)
        {
            return _context.Issues.Any(e => e.Id == id);
        }
    }
}
