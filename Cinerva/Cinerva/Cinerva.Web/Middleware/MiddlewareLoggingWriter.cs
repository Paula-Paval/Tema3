using Serilog;


namespace Cinerva.Web.Middleware
{
    public class MiddlewareLoggingWriter:IMiddlewareLoggingWriter
    {
        private readonly ILogger logger = Log.ForContext<MiddlewareLoggingWriter>();
       

        public void Write (string message)
        {
            logger.Information(message);          
        }

                    
    }
}
