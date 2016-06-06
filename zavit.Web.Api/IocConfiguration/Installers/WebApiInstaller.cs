using System.Web.Http.Controllers;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using zavit.Web.Api.Controllers;
using zavit.Web.Api.DtoServices.Places;

namespace zavit.Web.Api.IocConfiguration.Installers
{
    public class WebApiInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromAssemblyContaining<PlacesController>().BasedOn<IHttpController>().LifestyleTransient(),
                Component.For<IPlaceDtoService>().ImplementedBy<PlaceDtoService>().LifestyleTransient()
            );
        }
    }
}