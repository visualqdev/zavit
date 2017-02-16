using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using zavit.Domain.Shared.Images;
using zavit.Infrastructure.Images;

namespace zavit.Infrastructure.Ioc.Infrastructure
{
    public class InfrastructureImagesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IImageResizer>().ImplementedBy<ImageResizer>().LifestyleSingleton());
        }
    }
}