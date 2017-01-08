using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using zavit.Domain.Profiles;
using zavit.Infrastructure.Profiles;

namespace zavit.Infrastructure.Ioc.Infrastructure
{
    public class InfrastructureProfilesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IProfileRepository>().ImplementedBy<ProfileRepository>().LifestyleTransient(),
                Component.For<IProfileImageRepository>().ImplementedBy<ProfileImageRepository>().LifestyleTransient());
        }
    }
}