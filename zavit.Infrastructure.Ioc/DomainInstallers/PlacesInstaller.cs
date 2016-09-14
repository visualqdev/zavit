using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using zavit.Domain.Places;
using zavit.Domain.Places.Suggestions;
using zavit.Domain.Places.VenuePlaces;
using zavit.Domain.Places.VenuePlaces.DefaultVenues;

namespace zavit.Infrastructure.Ioc.DomainInstallers
{
    public class PlacesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IPlaceService>().ImplementedBy<PlaceService>().LifestyleTransient(),
                Component.For<IVenuePlaceCreator>().ImplementedBy<VenuePlaceCreator>().LifestyleTransient(),
                Component.For<IPlaceSuggestionsMerger>().ImplementedBy<PlaceSuggestionsMerger>().LifestyleTransient(),
                Component.For<IPublicPlaceSuggestionFactory>().ImplementedBy<PublicPlaceSuggestionFactory>().LifestyleTransient(),
                Component.For<IDefaultVenueProvider>().ImplementedBy<DefaultVenueProvider>().LifestyleTransient()
            );
        }
    }
}