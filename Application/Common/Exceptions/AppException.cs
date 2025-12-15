using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class AppException(string publicMessage, int statusCode, string? internalMessage = null) : Exception(internalMessage ?? publicMessage)
    {
        public int StatusCode { get; } = statusCode;
        public string PublicMessage { get; } = publicMessage;
    }
}
