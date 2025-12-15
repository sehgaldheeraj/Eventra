using Microsoft.AspNetCore.Http;

namespace Application.Common.Exceptions
{
    public class NotFoundException(string entityName, object key) : AppException(
            publicMessage: $"{entityName} not found.",
            statusCode: StatusCodes.Status404NotFound,
            internalMessage: $"{entityName} with ID '{key}' was not found."
            )
    {
    }
}