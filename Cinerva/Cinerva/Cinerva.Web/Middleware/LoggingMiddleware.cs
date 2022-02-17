using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;


namespace Cinerva.Web.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate next;      

        public LoggingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
               
        public async Task InvokeAsync(HttpContext httpContext, IMiddlewareLoggingWriter writer)
        {

            var userAgent=httpContext.Request.Headers.UserAgent.ToString();
            var statusCode = httpContext.Response.StatusCode.ToString();

            writer.Write(userAgent);    
            
            await this.next(httpContext);

            writer.Write(statusCode);
        }

    }
}
