using Microsoft.AspNetCore.Builder;

namespace Cinerva.Web.Middleware
{
    public static class LoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoggingMidleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggingMiddleware>();
        }
    }
}
