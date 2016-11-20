using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using zavit.Domain.Venues.PublicPlaces;
using zavit.Infrastructure.Places;
using zavit.Infrastructure.Places.PublicPlacesApis;
using zavit.Infrastructure.Places.PublicPlacesApis.Details;
using zavit.Infrastructure.Places.PublicPlacesApis.Search;

namespace zavit.Infrastructure.Ioc.Infrastructure
{
    public class InfrastructurePlacesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IPublicPlacesService>().ImplementedBy<PublicPlacesService>().LifestyleTransient(),
                Component.For<IGooglePlacesApi>().ImplementedBy<GooglePlacesApi>().LifestyleSingleton(),
                Component.For<IPlaceSearchResultsTransformer>().ImplementedBy<PlaceSearchResultsTransformer>().LifestyleTransient(),
                Component.For<IPlaceSearchTransformer>().ImplementedBy<PlaceSearchTransformer>().LifestyleTransient(),
                Component.For<IPlaceDetailsResultTransformer>().ImplementedBy<PlaceDetailsResultTransformer>().LifestyleTransient()
            );
        }
    }
}