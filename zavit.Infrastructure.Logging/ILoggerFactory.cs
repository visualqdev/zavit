using zavit.Domain.Shared;

namespace zavit.Infrastructure.Logging
{
    public interface ILoggerFactory
    {
        ILogger GetLogger(string loggerName);
    }
}