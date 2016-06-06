using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using zavit.Domain.Places;

namespace zavit.Infrastructure.Ioc.DomainInstallers
{
    public class PlacesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IPlaceService>().ImplementedBy<PlaceService>().LifestyleTransient()
            );
        }
    }
}