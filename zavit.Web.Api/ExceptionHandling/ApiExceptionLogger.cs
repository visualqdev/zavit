using System.Web.Http.ExceptionHandling;
using zavit.Infrastructure.Logging;

namespace zavit.Web.Api.ExceptionHandling
{
    public class ApiExceptionLogger : ExceptionLogger
    {
        readonly ILoggerFactory _loggerFactory;

        public ApiExceptionLogger(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public override void Log(ExceptionLoggerContext context)
        {
            _loggerFactory.GetLogger("ApiException").Error(context.Exception);
        }
    }
}