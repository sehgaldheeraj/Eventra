using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        public RegisterUserCommandHandler(IUserRepository userRepository) { 
            _userRepository = userRepository;
        }
        public async Task<Guid> Handle(RegisterUserCommand command, CancellationToken cancellationToken) {
            var user = new User
            {
                Name = command.Name,
                Email = command.Email,
                Password = command.Password,
                Role = command.Role,
            };
            await _userRepository.RegisterUserAsync(user);
            return user.Id;
        }
    }
}
