using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using zavit.Web.Api.Authorization.ClaimsIdentities;
using zavit.Web.Api.Controllers;
using zavit.Web.Api.DtoFactories.MessageRecipients;
using zavit.Web.Api.DtoFactories.Messaging.Messages;
using zavit.Web.Api.DtoFactories.Messaging.MessageThreadParticipants;
using zavit.Web.Api.DtoFactories.Messaging.MessageThreads;
using zavit.Web.Api.DtoFactories.ProfileImages;
using zavit.Web.Api.DtoFactories.Profiles;
using zavit.Web.Api.DtoFactories.VenueMembers;
using zavit.Web.Api.DtoFactories.VenueMemberships;
using zavit.Web.Api.DtoFactories.Venues;
using zavit.Web.Api.DtoServices.Accounts;
using zavit.Web.Api.DtoServices.MessageRecipients;
using zavit.Web.Api.DtoServices.Messaging.Messages;
using zavit.Web.Api.DtoServices.Messaging.MessageThreads;
using zavit.Web.Api.DtoServices.Messaging.MessageThreads.NewMessages;
using zavit.Web.Api.DtoServices.Messaging.MessageThreads.NewMessageThreads;
using zavit.Web.Api.DtoServices.ProfileImages;
using zavit.Web.Api.DtoServices.Profiles;
using zavit.Web.Api.DtoServices.VenueMembers;
using zavit.Web.Api.DtoServices.VenueMemberships;
using zavit.Web.Api.DtoServices.VenueMemberships.NewVenueMemberships;
using zavit.Web.Api.DtoServices.Venues;
using zavit.Web.Api.DtoServices.Venues.NewVenues;
using zavit.Web.Authorization.Controllers;

namespace zavit.Web.Api.IocConfiguration.Installers
{
    public class WebApiInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromAssemblyContaining<AccountsController>().BasedOn<IHttpController>().LifestyleTransient(),
                Classes.FromAssemblyContaining<ClaimsIdentityFilter>().BasedOn<IActionFilter>().LifestyleSingleton(),
                Component.For<ExternalAccountsController>().LifestyleTransient(),
                Component.For<IVenueDtoService>().ImplementedBy<VenueDtoService>().LifestyleTransient(),
                Component.For<IVenueDtoFactory>().ImplementedBy<VenueDtoFactory>().LifestyleTransient(),
                Component.For<IVenueDetailsDtoFactory>().ImplementedBy<VenueDetailsDtoFactory>().LifestyleTransient(),
                Component.For<IVenueActivityDtoFactory>().ImplementedBy<VenueActivityDtoFactory>().LifestyleTransient(),
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
                Component.For<IMessageDtoService>().ImplementedBy<MessageDtoService>().LifestyleTransient(),
                Component.For<INewMessageRequestProvider>().ImplementedBy<NewMessageRequestProvider>().LifestyleTransient(),
                Component.For<INewMessageThreadRequestProvider>().ImplementedBy<NewMessageThreadRequestProvider>().LifestyleTransient(),
                Component.For<IMessageCollectionDtoFactory>().ImplementedBy<MessageCollectionDtoFactory>().LifestyleTransient(),
                Component.For<IMessageRecipientDtoService>().ImplementedBy<MessageRecipientDtoService>().LifestyleTransient(),
                Component.For<IMessageRecipientDtoFactory>().ImplementedBy<MessageRecipientDtoFactory>().LifestyleTransient(),
                Component.For<IInboxThreadDtoFactory>().ImplementedBy<InboxThreadDtoFactory>().LifestyleTransient(),
                Component.For<IInboxThreadDetailsDtoFactory>().ImplementedBy<InboxThreadDetailsDtoFactory>().LifestyleTransient(),
                Component.For<IMessageRecipientCollectionDtoFactory>().ImplementedBy<MessageRecipientCollectionDtoFactory>().LifestyleTransient(),
                Component.For<IProfileDtoService>().ImplementedBy<ProfileDtoService>().LifestyleTransient(),
                Component.For<IProfileDtoFactory>().ImplementedBy<ProfileDtoFactory>().LifestyleTransient(),
                Component.For<IProfileUpdateFactory>().ImplementedBy<ProfileUpdateFactory>().LifestyleTransient(),
                Component.For<IAccountProfileRegistrationFactory>().ImplementedBy<AccountProfileRegistrationFactory>().LifestyleTransient(),
                Component.For<IAccountRegistrationDtoService>().ImplementedBy<AccountRegistrationDtoService>().LifestyleTransient(),
                Component.For<IProfileImageDtoService>().ImplementedBy<ProfileImageDtoService>().LifestyleTransient(),
                Component.For<IProfileImageUploadDtoFactory>().ImplementedBy<ProfileImageUploadDtoFactory>().LifestyleTransient()
                );
        }
    }
}