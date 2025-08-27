using Million.Application.Common.Exceptions;
using Serilog.Context;
using System.Net;
using System.Text.Json;

namespace Million.API.Middlewares
{
    /// <summary>
    /// Global exception handling middleware
    /// </summary>
    public class GlobalException
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalException> _logger;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public GlobalException(RequestDelegate next, ILogger<GlobalException> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Invoke function
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            LogContext.PushProperty("User", context.User?.Identity?.Name ?? "Anonymous");
            LogContext.PushProperty("Path", context.Request.Path);
            LogContext.PushProperty("TraceId", context.TraceIdentifier);

            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Resource not found.");
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync(JsonSerializer.Serialize(new { error = ex.Message }));
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validation error.");
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync(JsonSerializer.Serialize(new { error = ex.Message, details = ex.Errors }));
            }
            catch (ForbiddenAccessException ex)
            {
                _logger.LogWarning(ex, "Access denied.");
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync(JsonSerializer.Serialize(new { error = ex.Message }));
            }
            catch (InvalidCredentialsException ex)
            {
                _logger.LogWarning(ex, "Invalid credentials.");
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync(JsonSerializer.Serialize(new { error = ex.Message }));
            }
            catch (FluentValidation.ValidationException ex)
            {
                _logger.LogWarning(ex, "Error in validation.");

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                var errors = ex.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );

                var response = new
                {
                    error = "The request has validation errors.",
                    errors,
                    traceId = context.TraceIdentifier
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred while processing request.");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = new
                {
                    error = "Unhandled exception occurred.",
                    traceId = context.TraceIdentifier
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
