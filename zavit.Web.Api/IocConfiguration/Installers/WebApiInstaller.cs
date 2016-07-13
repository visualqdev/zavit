using System.Web.Http.Controllers;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using zavit.Web.Api.Controllers;
using zavit.Web.Api.DtoFactories.Places;
using zavit.Web.Api.DtoFactories.Venues;
using zavit.Web.Api.DtoServices.Places;
using zavit.Web.Api.DtoServices.Venues;

namespace zavit.Web.Api.IocConfiguration.Installers
{
    public class WebApiInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromAssemblyContaining<PlacesController>().BasedOn<IHttpController>().LifestyleTransient(),
                Component.For<IPlaceDtoService>().ImplementedBy<PlaceDtoService>().LifestyleTransient(),
                Component.For<IPlaceDtoFactory>().ImplementedBy<PlaceDtoFactory>().LifestyleTransient(),
                Component.For<IVenueDtoService>().ImplementedBy<VenueDtoService>().LifestyleTransient(),
                Component.For<IVenueDtoFactory>().ImplementedBy<VenueDtoFactory>().LifestyleTransient()
                );
        }
    }
}