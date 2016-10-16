using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using zavit.Web.Api.Authorization;
using zavit.Web.Api.Authorization.AccessAuthorization;
using zavit.Web.Api.Authorization.ClaimsIdentities;
using zavit.Web.Api.Controllers;
using zavit.Web.Api.DtoFactories.Messaging.Messages;
using zavit.Web.Api.DtoFactories.Messaging.MessageThreadParticipants;
using zavit.Web.Api.DtoFactories.Messaging.MessageThreads;
using zavit.Web.Api.DtoFactories.Places;
using zavit.Web.Api.DtoFactories.VenueMembers;
using zavit.Web.Api.DtoFactories.VenueMemberships;
using zavit.Web.Api.DtoFactories.Venues;
using zavit.Web.Api.DtoServices.Messaging.MessageThreads;
using zavit.Web.Api.DtoServices.Messaging.MessageThreads.NewMessages;
using zavit.Web.Api.DtoServices.Messaging.MessageThreads.NewMessageThreads;
using zavit.Web.Api.DtoServices.Places;
using zavit.Web.Api.DtoServices.VenueMembers;
using zavit.Web.Api.DtoServices.VenueMemberships;
using zavit.Web.Api.DtoServices.VenueMemberships.NewVenueMemberships;
using zavit.Web.Api.DtoServices.Venues;
using zavit.Web.Api.DtoServices.Venues.NewVenues;
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
                Classes.FromAssemblyContaining<ClaimsIdentityFilter>().BasedOn<IActionFilter>().LifestyleSingleton(),
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
                Component.For<IUserContextFactory>().AsFactory(),
                Component.For<ILocalAccessTokenProvider>().ImplementedBy<LocalAccessTokenProvider>().LifestyleTransient(),
                Component.For<IVenueDetailsDtoFactory>().ImplementedBy<VenueDetailsDtoFactory>().LifestyleTransient(),
                Component.For<IVenueActivityDtoFactory>().ImplementedBy<VenueActivityDtoFactory>().LifestyleTransient(),
                Component.For<IAuthenticationOptionsFactory>().ImplementedBy<AuthenticationOptionsFactory>().LifestyleSingleton(),
                Component.For<IExternalLoginDataProvider>().ImplementedBy<ExternalLoginDataProvider>().LifestyleSingleton(),
                Component.For<INewVenueProvider>().ImplementedBy<NewVenueProvider>().LifestyleTransient(),
                Component.For<IVenueMembershipDtoService>().ImplementedBy<VenueMembershipDtoService>().LifestyleTransient(),
                Component.For<IVenueMembershipDtoFactory>().ImplementedBy<VenueMembershipDtoFactory>().LifestyleTransient(),
                Component.For<IMembershipVenueDtoFactory>().ImplementedBy<MembershipVenueDtoFactory>().LifestyleTransient(),
                Component.For<INewVenueMembershipProvider>().ImplementedBy<NewVenueMembershipProvider>().LifestyleTransient(),
                Component.For<IVenueMembershipDetailsDtoFactory>().ImplementedBy<VenueMembershipDetailsDtoFactory>().LifestyleTransient(),
                Component.For<IVenueMembershipDetailsDtoService>().ImplementedBy<VenueMembershipDetailsDtoService>().LifestyleTransient(),
                Component.For<IVenueMemberDtoService>().ImplementedBy<VenueMemberDtoService>().LifestyleTransient(),
                Component.For<IVenueMemberCollectionDtoFactory>().ImplementedBy<VenueMemberCollectionDtoFactory>().LifestyleTransient(),
                Component.For<IVenueMemberDtoFactory>().ImplementedBy<VenueMemberDtoFactory>().LifestyleTransient(),
                Component.For<IMessageDtoFactory>().ImplementedBy<MessageDtoFactory>().LifestyleTransient(),
                Component.For<IThreadParticipantDtoFactory>().ImplementedBy<ThreadParticipantDtoFactory>().LifestyleTransient(),
                Component.For<IMessageThreadDtoFactory>().ImplementedBy<MessageThreadDtoFactory>().LifestyleTransient(),
                Component.For<INewMessageThreadDtoFactory>().ImplementedBy<NewMessageThreadDtoFactory>().LifestyleTransient(),
                Component.For<IMessageThreadDtoService>().ImplementedBy<MessageThreadDtoService>().LifestyleTransient(),
                Component.For<INewMessageRequestProvider>().ImplementedBy<NewMessageRequestProvider>().LifestyleTransient(),
                Component.For<INewMessageThreadRequestProvider>().ImplementedBy<NewMessageThreadRequestProvider>().LifestyleTransient()
                );
        }
    }
}