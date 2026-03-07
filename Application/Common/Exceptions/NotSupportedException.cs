using Microsoft.AspNetCore.Http;

namespace Application.Common.Exceptions
{
    public class NotSupportedException(Type type) : AppException(
            publicMessage: "Operation not supported.",
            statusCode: StatusCodes.Status400BadRequest,
            internalMessage: $"No handler or factory mapping found for type '{type.Name}'."
        )
    {
    }
}