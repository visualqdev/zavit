using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using zavit.Domain.VenueMemberships;
using zavit.Domain.VenueMemberships.NewVenueMembershipCreation;

namespace zavit.Infrastructure.Ioc.DomainInstallers
{
    public class VenueMembershipsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IVenueMembershipService>().ImplementedBy<VenueMembershipService>().LifestyleTransient(),
                Component.For<IVenueMembershipCreator>().ImplementedBy<VenueMembershipCreator>().LifestyleTransient()
                );
        }
    }
}