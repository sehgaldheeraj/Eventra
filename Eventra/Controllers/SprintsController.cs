using Application.Common.Responses;
using Application.Sprints.Commands.CreateSprint;
using Application.Sprints.Commands.UpdateSprint;
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
        public async Task<ActionResult<IEnumerable<Sprint>>> GetSprints()
        {
            return await _context.Sprints.ToListAsync();
        }

        // GET: api/Sprints/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sprint>> GetSprint(Guid id)
        {
            var sprint = await _context.Sprints.Include(s => s.Project).FirstOrDefaultAsync(u => u.Id == id);

            if (sprint == null)
            {
                return NotFound();
            }

            return sprint;
        }

        // PUT: api/Sprints/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchSprint(Guid id, [FromBody]UpdateSprintDto dto)
        {
            var command = new UpdateSprintCommand(
                id, dto.Title, dto.Goal, dto.StartDate, dto.EndDate, dto.ProjectId, dto.Status
            );
            await _mediator.Send(command);
            return NoContent();
        }

        // POST: api/Sprints
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResponse<Guid>>> PostSprint(CreateSprintCommand command)
        {
            var id = await _mediator.Send(command);
            var response = ApiResponse<Guid>.SuccessResponse(id, "Sprint created successfully");
            return CreatedAtAction("GetSprint", new { id }, response);
        }

        // DELETE: api/Sprints/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSprint(Guid id)
        {
            var sprint = await _context.Sprints.FindAsync(id);
            if (sprint == null)
            {
                return NotFound();
            }

            _context.Sprints.Remove(sprint);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SprintExists(Guid id)
        {
            return _context.Sprints.Any(e => e.Id == id);
        }
    }
}
