using Api_Pagination_Sorting_Demo.Dtos.Common;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace Api_Pagination_Sorting_Demo.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public GlobalExceptionMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionMiddleware> logger,
            IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                
            }
        }  
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "An unhandled exception occurred.",context.TraceIdentifier);

            if(context.Response.HasStarted)
            {
                throw exception;
            }
            context.Response.ContentType = "application/json";
            ApiErrorResponseDto response;
            switch (exception)
            {
                case DbUpdateException:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response = CreateErroResponse(context, "Database operation failed", "DatabaseError", exception);
                    break;

                case TimeoutException:
                    context.Response.StatusCode = (int)HttpStatusCode.RequestTimeout;
                    response = CreateErroResponse(context, "The request timed out.please try again", "TimeoutError", exception);
                    break;
                case ArgumentException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = CreateErroResponse(context, "Invalid input was provided", "ArgumentError", exception);
                    break;

                case  KeyNotFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    response = CreateErroResponse(context, "The requested resource was not found", "NotFoundError", exception);
                    break;

                default:context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response = CreateErroResponse(context, "An unexpected error occurred. Please try again later.", "ServerError", exception);
                    break;
            }
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            string jsonResponse = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(jsonResponse);
        }

        private ApiErrorResponseDto CreateErroResponse(HttpContext context,
            string message,string errorType, Exception exception)
        {
            return new ApiErrorResponseDto
            {
                StatusCode = context.Response.StatusCode,
                Message = message,

                ErrorType = errorType,
                TraceId = context.TraceIdentifier,
                Details = _env.IsDevelopment() ? exception.Message : null
            };
            }
        }
    }

