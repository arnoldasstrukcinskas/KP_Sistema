using KP_Sistema.BLL.Exceptions.Authentication;
using KP_Sistema.BLL.Exceptions.Community;
using KP_Sistema.BLL.Exceptions.UtilityTasks;
using KP_Sistema.BLL.Services;
using System.Security.Authentication;

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

                    //UtilityTasks Exceptions
                    UtilityTaskNotFoundException => StatusCodes.Status404NotFound,
                    UtilityTaskUnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                    UtilityTaskException => StatusCodes.Status400BadRequest,

                    //Authentication Exceptions
                    UserAuthenticationNotFoundException => StatusCodes.Status404NotFound,
                    UserAuthenticationRegistrationFailedException => StatusCodes.Status406NotAcceptable,
                    UserAuthenticationException => StatusCodes.Status403Forbidden,

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
