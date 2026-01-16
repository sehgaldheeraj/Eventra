using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Contexts
{
    public interface IUserContext
    {
        Guid UserId { get; }
        string? Role {  get; }
        bool IsAuthenticated { get; }
    }
}
