using KP_Sistema.BLL.Exceptions;
using KP_Sistema.BLL.Services;

namespace KP_Sistema.API.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                int statusCode = ex switch
                {
                    //Community Exceptions
                    CommunityNotFoundException => StatusCodes.Status404NotFound,
                    CommunityUnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                    CommunityException => StatusCodes.Status400BadRequest,

                    //UtilityTasks exceptions


                    //Default exception
                    _ => StatusCodes.Status500InternalServerError
                };

                var errorMessage = ex.Message;

                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsJsonAsync(new { error = errorMessage });
            }

        }
    }
}
