using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using zavit.Domain.Accounts;
using zavit.Infrastructure.Accounts.Repositories;

namespace zavit.Infrastructure.Ioc.Infrastructure
{
    public class InfrastructureAccountsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IAccountRepository>().ImplementedBy<AccountRepository>().LifestyleTransient());
        }
    }
}