using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using zavit.Domain.Messaging.MessageReads;
using zavit.Domain.Messaging.Messages;
using zavit.Web.Api;
using zavit.Web.Mvc.Settings;
using zavit.Web.Mvc.SignalR.ConnectionIds;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting.DtoFactories;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting.GroupIds;
using zavit.Web.Mvc.SignalR.Messaging.Observers;

namespace zavit.Web.Mvc.IocConfiguration.Installers
{
    public class WebMvcInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly().InSameNamespaceAs<GoogleApiSettings>().WithServiceFirstInterface().LifestyleSingleton(),
                Component.For<IMessageSentObserver>().ImplementedBy<MessageSentSignalRObserver>().LifestyleTransient(),
                Component.For<ApiStartup>().LifestyleTransient(),
                Component.For<IThreadMessageBroadcastRequestFactory>().ImplementedBy<ThreadMessageBroadcastRequestFactory>().LifestyleTransient(),
                Component.For<IInboxMessageBroadcastRequestFactory>().ImplementedBy<InboxMessageBroadcastRequestFactory>().LifestyleTransient(),
                Component.For<IConnectionIdProvider>().ImplementedBy<ConnectionIdProvider>().LifestyleSingleton(),
                Component.For<IMessagingBroadcaster>().ImplementedBy<MessagingBroadcaster>().LifestyleSingleton(),
                Component.For<IMessageReadObserver>().ImplementedBy<MessageReadSignalRObserver>().LifestyleTransient(),
                Component.For<IReadMessagesBroadcastRequestsProvider>().ImplementedBy<ReadMessagesBroadcastRequestsProvider>().LifestyleTransient(),
                Component.For<IReadMessagesBroadcastDtoFactory>().ImplementedBy<ReadMessagesBroadcastDtoFactory>().LifestyleTransient(),
                Component.For<IReadMessagesDtoFactory>().ImplementedBy<ReadMessagesDtoFactory>().LifestyleTransient(),
                Component.For<IThreadGroupIdProvider>().ImplementedBy<ThreadGroupIdProvider>().LifestyleSingleton(),
                Component.For<IInboxGroupIdProvider>().ImplementedBy<InboxGroupIdProvider>().LifestyleSingleton()
            );
        }
    }
}