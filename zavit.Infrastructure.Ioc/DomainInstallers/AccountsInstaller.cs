using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using zavit.Domain.Accounts;
using zavit.Domain.Accounts.Registrations;
using zavit.Domain.Accounts.Registrations.Validators;

namespace zavit.Infrastructure.Ioc.DomainInstallers
{
    public class AccountsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IAccountRegistrationResultFactory>()
                    .ImplementedBy<AccountRegistrationResultFactory>()
                    .LifestyleTransient(),
                Component.For<IAccountCreator>().ImplementedBy<AccountCreator>().LifestyleTransient(),
                Component.For<IAccountService>().ImplementedBy<AccountService>().LifestyleTransient(),
                Classes.FromAssemblyContaining<IAccountRegistrationValidator>()
                    .BasedOn<IAccountRegistrationValidator>()
                    .WithServiceFirstInterface()
                    .LifestyleTransient());
        }
    }
}