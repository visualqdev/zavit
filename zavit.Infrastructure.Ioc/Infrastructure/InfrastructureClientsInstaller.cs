using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using zavit.Domain.Clients;
using zavit.Domain.Clients.Tokens;
using zavit.Infrastructure.Clients;
using zavit.Infrastructure.Clients.Tokens;

namespace zavit.Infrastructure.Ioc.Infrastructure
{
    public class InfrastructureClientsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                    Component.For<IRefreshTokenRepository>().ImplementedBy<RefreshTokenRepository>().LifestyleTransient(),
                    Component.For<IClientRepository>().ImplementedBy<ClientRepository>().LifestyleTransient(),
                    Component.For<ITokenSecurity>().ImplementedBy<TokenSecurity>().LifestyleSingleton()
                );
        }
    }
}