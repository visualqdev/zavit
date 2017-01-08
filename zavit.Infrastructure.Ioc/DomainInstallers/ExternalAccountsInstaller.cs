using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using zavit.Domain.ExternalAccounts;
using zavit.Domain.ExternalAccounts.Registrations;

namespace zavit.Infrastructure.Ioc.DomainInstallers
{
    public class ExternalAccountsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IExternalAccountService>().ImplementedBy<ExternalAccountService>().LifestyleTransient(),
                Component.For<INewExternalAccountProvider>().ImplementedBy<NewExternalAccountProvider>().LifestyleTransient()
                );
        }
    }
}