using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using zavit.Web.Api.Authorization;
using zavit.Web.Api.Authorization.ClaimsIdentities;
using zavit.Web.Api.Authorization.ResourcesAuthorization;
using zavit.Web.Authorization;
using zavit.Web.Authorization.ExternalLogins;
using zavit.Web.Authorization.ExternalLogins.LoginData;
using zavit.Web.Core.Context;

namespace zavit.Web.Api.IocConfiguration.Installers
{
    public class AuthorizationInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IAccountRepositoryFactory>().AsFactory(),
                Component.For<IClientRepositoryFactory>().AsFactory(),
                Component.For<IRefreshTokenProviderFactory>().AsFactory(),
                Component.For<IRefreshTokenRepositoryFactory>().AsFactory(),
                Component.For<IUserContext>().ImplementedBy<UserContext>().LifestylePerWebRequest(),
                Component.For<IClaimsIdentityProvider>().ImplementedBy<ClaimsIdentityProvider>().LifestylePerWebRequest(),
                Component.For<IClaimsIdentityProviderFactory>().AsFactory(),
                Component.For<IUserContextFactory>().AsFactory(),
                Component.For<ILocalAccessTokenProvider>().ImplementedBy<LocalAccessTokenProvider>().LifestyleTransient(),
                Component.For<IAuthenticationOptionsFactory>().ImplementedBy<AuthenticationOptionsFactory>().LifestyleSingleton(),
                Component.For<IExternalLoginDataProvider>().ImplementedBy<ExternalLoginDataProvider>().LifestyleSingleton(),
                Classes.FromAssemblyContaining<IResourceAuthorization>().BasedOn<IResourceAuthorization>().WithServiceFirstInterface().LifestyleTransient(),
                Component.For<IResourceAuthorizationsProvider>().AsFactory()
                );
        }
    }
}