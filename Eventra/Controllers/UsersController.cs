using Application.Common.Responses;
using Application.Users.Commands.LoginUser;
using Application.Users.Commands.RegisterUser;
using Application.Users.DTOs;
using Application.Users.Queries.GetUserById;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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
        public async Task<ActionResult<ApiResponse<Guid>>> Register([FromBody] RegisterUserCommand request)
        {
            var id = await _mediator.Send(request);
            var response = ApiResponse<Guid>.SuccessResponse(
                id,
                "User registered successfully."
            );

            return CreatedAtAction(nameof(GetUserProfile), new { id = id }, response);
        }

        /// <summary>
        /// Logs in a user and returns JWT
        /// </summary>
        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<string>>> Login([FromBody] LoginUserCommand request)
        {
            var token = await _mediator.Send(request);
            var response = ApiResponse<string>.SuccessResponse(
                token,
                "Login successful."
            );

            return Ok(response);
        }

        /// <summary>
        /// Fetches user details based on their id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Profile>>> GetUserProfile(Guid id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(id));

            if (user == null)
                return NotFound(ApiResponse<string>.FailResponse("User not found."));

            var response = ApiResponse<Profile>.SuccessResponse(user);
            return Ok(response);
        }
    }
}
