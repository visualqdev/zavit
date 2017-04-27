using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using zavit.Infrastructure.Mailing;

namespace zavit.Infrastructure.Ioc.Infrastructure
{
    public class InfrastructureMailingInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IMailer>().ImplementedBy<Mailer>().LifestyleSingleton()
                );
        }
    }
}