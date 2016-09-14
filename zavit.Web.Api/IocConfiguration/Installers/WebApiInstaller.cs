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
using zavit.Web.Authorization;
using zavit.Web.Authorization.Controllers;
using zavit.Web.Authorization.ExternalLogins;
using zavit.Web.Authorization.ExternalLogins.LoginData;
using zavit.Web.Core.Context;

namespace zavit.Web.Api.IocConfiguration.Installers
{
    public class WebApiInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromAssemblyContaining<PlacesController>().BasedOn<IHttpController>().LifestyleTransient(), 
                Component.For<ExternalAccountsController>().LifestyleTransient(),
                Component.For<IPlaceDtoService>().ImplementedBy<PlaceDtoService>().LifestyleTransient(),
                Component.For<IPlaceDtoFactory>().ImplementedBy<PlaceDtoFactory>().LifestyleTransient(),
                Component.For<IVenueDtoService>().ImplementedBy<VenueDtoService>().LifestyleTransient(),
                Component.For<IVenueDtoFactory>().ImplementedBy<VenueDtoFactory>().LifestyleTransient(),
                Component.For<IAccountRepositoryFactory>().AsFactory(),
                Component.For<IClientRepositoryFactory>().AsFactory(),
                Component.For<IRefreshTokenProviderFactory>().AsFactory(),
                Component.For<IRefreshTokenRepositoryFactory>().AsFactory(),
                Component.For<IUserContext>().ImplementedBy<UserContext>().LifestylePerWebRequest(),
                Component.For<IClaimsIdentityProvider>().ImplementedBy<ClaimsIdentityProvider>().LifestylePerWebRequest(),
                Component.For<IClaimsIdentityProviderFactory>().AsFactory(),
                Component.For<AccessAuthorizationFilter>().LifestyleSingleton(),
                Component.For<ILocalAccessTokenProvider>().ImplementedBy<LocalAccessTokenProvider>().LifestyleTransient(),
                Component.For<IVenueDetailsDtoFactory>().ImplementedBy<VenueDetailsDtoFactory>().LifestyleTransient(),
                Component.For<IVenueActivityDtoFactory>().ImplementedBy<VenueActivityDtoFactory>().LifestyleTransient(),
                Component.For<IAuthenticationOptionsFactory>().ImplementedBy<AuthenticationOptionsFactory>().LifestyleSingleton(),
                Component.For<IExternalLoginDataProvider>().ImplementedBy<ExternalLoginDataProvider>().LifestyleSingleton());
        }
    }
}