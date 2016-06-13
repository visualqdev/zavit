using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using zavit.Domain.Places.PublicPlaces;
using zavit.Infrastructure.Places;
using zavit.Infrastructure.Places.PublicPlacesApis;

namespace zavit.Infrastructure.Ioc.Infrastructure
{
    public class InfrastructurePlacesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IPublicPlacesService>().ImplementedBy<PublicPlacesService>().LifestyleTransient(),
                Component.For<IGooglePlaceSearchApi>().ImplementedBy<GooglePlaceSearchApi>().LifestyleSingleton(),
                Component.For<IPublicPlacesTransformer>().ImplementedBy<PublicPlacesTransformer>().LifestyleTransient(),
                Component.For<IPublicPlaceTransformer>().ImplementedBy<PublicPlaceTransformer>().LifestyleTransient()
            );
        }
    }
}