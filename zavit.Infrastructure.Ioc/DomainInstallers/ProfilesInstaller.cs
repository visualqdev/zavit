using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using zavit.Domain.Profiles;
using zavit.Domain.Profiles.Registration;
using zavit.Domain.Profiles.Updating;

namespace zavit.Infrastructure.Ioc.DomainInstallers
{
    public class ProfilesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IProfileService>().ImplementedBy<ProfileService>().LifestyleTransient(),
                Component.For<IProfileCreator>().ImplementedBy<ProfileCreator>().LifestyleTransient(),
                Classes.FromAssemblyContaining<IProfileUpdater>().BasedOn<IProfileUpdater>().WithServiceFirstInterface().LifestyleTransient()
            );
        }
    }
}