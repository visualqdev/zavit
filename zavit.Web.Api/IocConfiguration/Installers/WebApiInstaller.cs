using System.Web.Http.Controllers;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using zavit.Web.Api.Authorization;
using zavit.Web.Api.Controllers;
using zavit.Web.Api.DtoFactories.Places;
using zavit.Web.Api.DtoFactories.Venues;
using zavit.Web.Api.DtoServices.Places;
using zavit.Web.Api.DtoServices.Venues;
using zavit.Web.Core.Authorization;
using zavit.Web.Core.Context;

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
                Component.For<IVenueDtoFactory>().ImplementedBy<VenueDtoFactory>().LifestyleTransient(),
                Component.For<IAccountRepositoryFactory>().AsFactory(),
                Component.For<IUserContext>().ImplementedBy<UserContext>().LifestylePerWebRequest(),
                Component.For<IClaimsIdentityProvider>().ImplementedBy<ClaimsIdentityProvider>().LifestylePerWebRequest(),
                Component.For<IClaimsIdentityProviderFactory>().AsFactory(),
                Component.For<AccessAuthorizationFilter>().LifestyleSingleton()
                );
        }
    }
}