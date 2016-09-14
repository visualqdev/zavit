using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using zavit.Domain.Venues;
using zavit.Infrastructure.Venues;

namespace zavit.Infrastructure.Ioc.Infrastructure
{
    public class InfrastructureVenuesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IVenueRepository>().ImplementedBy<VenueRepository>().LifestyleTransient());
        }
    }
}