using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }

        // Success with data and optional message
        public static ApiResponse<T> SuccessResponse(T data, string? message = null)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        // Success with message only (no data)
        public static ApiResponse<T> SuccessMessage(string message)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message
            };
        }

        // Failure with message and optional errors
        public static ApiResponse<T> FailResponse(string message, List<string>? errors = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Errors = errors
            };
        }

        // Failure with only errors (e.g., validation errors)
        public static ApiResponse<T> FailResponse(List<string> errors)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Errors = errors
            };
        }

        // Generic failure (optional overload)
        public static ApiResponse<T> FailResponse()
        {
            return new ApiResponse<T>
            {
                Success = false
            };
        }
    }
}
