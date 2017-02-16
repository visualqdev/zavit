using System;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using zavit.Domain.Shared;

namespace zavit.Infrastructure.Logging.Ioc
{
    public class LoggerDependencyResolver : ISubDependencyResolver
    {
        readonly Type _loggerType;
        readonly ILoggerFactory _loggerFactory;

        public LoggerDependencyResolver(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _loggerType = typeof(ILogger);
        }

        public object Resolve(
            CreationContext context,
            ISubDependencyResolver contextHandlerResolver,
            ComponentModel model,
            DependencyModel dependency)
        {
            var componentName = model.Implementation?.Name ?? model.Name;
            return _loggerFactory.GetLogger(componentName);
        }

        public bool CanResolve(
            CreationContext context,
            ISubDependencyResolver contextHandlerResolver,
            ComponentModel model,
            DependencyModel dependency)
        {
            return dependency.TargetType == _loggerType;
        }
    }
}