using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using zavit.Domain.Clients.Tokens;

namespace zavit.Infrastructure.Ioc.DomainInstallers
{
    public class ClientsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                    Component.For<IRefreshTokenProvider>().ImplementedBy<RefreshTokenProvider>().LifestyleTransient()
                );
        }
    }
}