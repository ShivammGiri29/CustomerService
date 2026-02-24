using Customer.Application.Exceptions;
using Customer_Service.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Customer_Service.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode statusCode;
            ApiError apiError;

            switch (ex)
            {
                // ✅ Custom Not Found
                case NotFoundException notFoundEx:
                    statusCode = HttpStatusCode.NotFound;
                    apiError = new ApiError
                    {
                        Code = "NOT_FOUND",
                        Details = notFoundEx.Message
                    };
                    break;

                // ✅ HttpClient 404 (GetFromJsonAsync case)
                case HttpRequestException httpEx
                    when httpEx.StatusCode == HttpStatusCode.NotFound:
                    statusCode = HttpStatusCode.NotFound;
                    apiError = new ApiError
                    {
                        Code = "NOT_FOUND",
                        Details = "Resource not found in external service"
                    };
                    break;

                // ✅ Bad Request
                case BadRequestException badRequestEx:
                    statusCode = HttpStatusCode.BadRequest;
                    apiError = new ApiError
                    {
                        Code = "BAD_REQUEST",
                        Details = badRequestEx.Message
                    };
                    break;

                // ✅ Database Errors
                case DbUpdateException dbEx:
                    statusCode = HttpStatusCode.BadRequest;
                    apiError = new ApiError
                    {
                        Code = "DATABASE_ERROR",
                        Details = dbEx.InnerException?.Message ?? dbEx.Message
                    };
                    break;

                // ✅ Any Other Error
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    apiError = new ApiError
                    {
                        Code = "INTERNAL_SERVER_ERROR",
                        Details = ex.Message
                    };
                    break;
            }

            _logger.LogError(ex, ex.Message);

            var response = ApiResponse<object>.FailureResponse(
                apiError.Details,
                apiError);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}