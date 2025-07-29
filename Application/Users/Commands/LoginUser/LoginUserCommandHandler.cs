using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public LoginUserCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<string> Handle(LoginUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(command.Email);
            if (user == null) { 
                throw new UnauthorizedAccessException("Invalid Credentials!");
            }
            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.Password, command.Password);

            if (result != PasswordVerificationResult.Success)
                throw new UnauthorizedAccessException("Invalid credentials");

            return _jwtTokenGenerator.GenerateToken(user);

        }
    }
}
