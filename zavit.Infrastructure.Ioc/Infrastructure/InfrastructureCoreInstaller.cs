using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using zavit.Infrastructure.Core.Serialization;

namespace zavit.Infrastructure.Ioc.Infrastructure
{
    public class InfrastructureCoreInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IJsonSerializer>().ImplementedBy<JsonSerializerWrapper>().LifestyleSingleton()
            );
        }
    }
}