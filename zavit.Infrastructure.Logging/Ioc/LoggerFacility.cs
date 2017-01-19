using Castle.MicroKernel.Facilities;

namespace zavit.Infrastructure.Logging.Ioc
{
    public class LoggerFacility : AbstractFacility
    {
        protected override void Init()
        {
            Kernel.Resolver.AddSubResolver(new LoggerDependencyResolver());
        }
    }
}