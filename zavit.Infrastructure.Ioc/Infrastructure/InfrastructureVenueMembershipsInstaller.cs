using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using zavit.Domain.VenueMemberships;
using zavit.Infrastructure.VenueMemberships.Repositories;

namespace zavit.Infrastructure.Ioc.Infrastructure
{
    public class InfrastructureVenueMembershipsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IVenueMembershipRepository>().ImplementedBy<VenueMembershipRepository>().LifestyleTransient()
                );
        }
    }
}