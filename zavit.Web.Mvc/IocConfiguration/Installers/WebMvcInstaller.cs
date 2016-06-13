using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using zavit.Web.Mvc.Settings;

namespace zavit.Web.Mvc.IocConfiguration.Installers
{
    public class WebMvcInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly().InSameNamespaceAs<GoogleApiSearchSettings>().WithServiceFirstInterface().LifestyleSingleton()
            );
        }
    }
}