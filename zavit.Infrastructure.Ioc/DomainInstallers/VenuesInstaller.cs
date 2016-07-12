using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using zavit.Domain.Venues;
using zavit.Domain.Venues.NewVenueCreation;

namespace zavit.Infrastructure.Ioc.DomainInstallers
{
    public class VenuesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IVenueService>().ImplementedBy<VenueService>().LifestyleTransient(),
                Component.For<IVenueCreator>().ImplementedBy<VenueCreator>().LifestyleTransient()
            );
        }
    }
}