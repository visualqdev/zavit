using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using zavit.Domain.Profiles;
using zavit.Domain.Profiles.ProfileImages;
using zavit.Infrastructure.Profiles;
using zavit.Infrastructure.Profiles.ProfileImages;

namespace zavit.Infrastructure.Ioc.Infrastructure
{
    public class InfrastructureProfilesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IProfileRepository>().ImplementedBy<ProfileRepository>().LifestyleTransient(),
                Component.For<IProfileImageStorage>().ImplementedBy<ProfileImageStorage>().LifestyleTransient());
        }
    }
}