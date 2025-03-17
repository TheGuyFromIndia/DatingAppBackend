using DatingApp.Errors;
using System.Net;
using System.Text.Json;

namespace DatingApp.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment environment)
    {
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex) {
                logger.LogError(ex, ex.Message);
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                var respone = environment.IsDevelopment() ? new ApiException(httpContext.Response.StatusCode, ex.Message, null) :
                    new ApiException(httpContext.Response.StatusCode, ex.Message, "Internal server error")
                    ;

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };

                var json = JsonSerializer.Serialize(respone, options);

                await httpContext.Response.WriteAsync(json);
            }
        }
    }
}
