using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using zavit.Domain.Activities;
using zavit.Infrastructure.Activities;

namespace zavit.Infrastructure.Ioc.Infrastructure
{
    public class InfrastructureActivitiesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IActivityRepository>().ImplementedBy<ActivityRepository>().LifestyleTransient());
        }
    }
}