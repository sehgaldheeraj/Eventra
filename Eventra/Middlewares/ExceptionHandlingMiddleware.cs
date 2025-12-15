using Application.Common.Exceptions;
using Application.Common.Responses;
using FluentValidation;
using System.Text.Json;

namespace Eventra.Middlewares
{
    public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AppException ex)
            {
                // Custom app exceptions (BadRequest, NotFound, Conflict, Unauthorized, Forbidden)
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = ex.StatusCode;

                var response = ApiResponse<object>.FailResponse(
                    ex.PublicMessage,
                    [ex.Message]
                );

                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
            catch (ValidationException ex)
            {
                // FluentValidation errors
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var errors = ex.Errors.Select(e => e.ErrorMessage).ToList();

                var response = ApiResponse<object>.FailResponse(
                    "Validation failed.",
                    errors
                );

                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
            catch (Exception ex)
            {
                // Unknown/unexpected server errors
                _logger.LogError(ex, "Unhandled exception");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var response = ApiResponse<object>.FailResponse(
                    "An unexpected error occurred.",
                    [ex.Message]
                );

                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
