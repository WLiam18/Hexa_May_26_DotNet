using System.Diagnostics;

namespace Api_Pagination_Sorting_Demo.Middlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;
        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            _logger.LogInformation(
                "Handling request started: {Method} {Path} TraceId: {TraceId}",
                context.Request.Method,
                context.Request.Path, context.TraceIdentifier);

            await _next(context);
            stopwatch.Stop();
            _logger.LogInformation(
                               "HTTP Response completed : {Method} {Path}" +
                               " TraceId: {TraceId} Duration: {Duration}ms",                               
                               context.Request.Method,                               
                               context.Request.Path,
                               context.Response.StatusCode, stopwatch.ElapsedMilliseconds);
        }
    }
    }

