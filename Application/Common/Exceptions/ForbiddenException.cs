using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class ForbiddenException(string publicMessage = "Forbidden Access") : AppException(publicMessage, StatusCodes.Status403Forbidden)
    {
    }
}
