using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;

namespace zavit.Infrastructure.Logging.Ioc
{
    public class LoggerFacility : AbstractFacility
    {
        protected override void Init()
        {
            Kernel.Register(Component.For<ILoggerFactory>().ImplementedBy<LoggerFactory>().LifestyleSingleton());
            Kernel.Resolver.AddSubResolver(new LoggerDependencyResolver(Kernel.Resolve<ILoggerFactory>()));
        }
    }
}