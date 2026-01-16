using Application.Common.Interfaces.Contexts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Auth
{
    public class UserContext(HttpContextAccessor accessor) : IUserContext
    {
        private readonly HttpContextAccessor _accessor = accessor;
        public bool IsAuthenticated 
            => _accessor.HttpContext.User?.Identity?.IsAuthenticated == true;
        public Guid UserId
            => Guid.Parse(
                _accessor.HttpContext!
                    .User
                    .FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new UnauthorizedAccessException("User not authenticated")
            );
        public string? Role =>
            _accessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);
    }
}
