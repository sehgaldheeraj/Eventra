using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<Guid>
    {
        
        public required string Name { get; set; }
        [Required]
        [MaxLength(30)]
        public required string Password { get; set; }
        [Required]
        [MaxLength(30)]
        public required string ConfirmPassword { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Role { get; set; }
    }
}
