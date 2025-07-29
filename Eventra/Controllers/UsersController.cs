using Application.Users.Commands.LoginUser;
using Application.Users.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Eventra.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Registers a new user
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand request)
        {
            var id = await _mediator.Send(request);
            return CreatedAtAction(nameof(Register), new { id }, null);
        }

        /// <summary>
        /// Logs in a user and returns JWT
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand request)
        {
            var token = await _mediator.Send(request);
            return Ok(new { Token = token });
        }
    }
}
