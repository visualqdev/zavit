using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using zavit.Domain.Venues;
using zavit.Domain.Venues.DefaultVenues;
using zavit.Domain.Venues.NewVenueCreation;
using zavit.Domain.Venues.Search;

namespace zavit.Infrastructure.Ioc.DomainInstallers
{
    public class VenuesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IVenueService>().ImplementedBy<VenueService>().LifestyleTransient(),
                Component.For<IVenueCreator>().ImplementedBy<VenueCreator>().LifestyleTransient(),
                Component.For<IVenueSuggestionsMerger>().ImplementedBy<VenueSuggestionsMerger>().LifestyleTransient(),
                Component.For<IPublicVenueSuggestionFactory>().ImplementedBy<PublicVenueSuggestionFactory>().LifestyleTransient(),
                Component.For<IDefaultVenueProvider>().ImplementedBy<DefaultVenueProvider>().LifestyleTransient()
            );
        }
    }
}