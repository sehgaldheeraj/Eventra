using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class ConflictException(string publicMessage, string? internalMessage = null) : AppException(publicMessage, StatusCodes.Status409Conflict, internalMessage)
    {
    }
}
