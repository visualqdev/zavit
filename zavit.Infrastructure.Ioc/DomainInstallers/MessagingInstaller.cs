using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using zavit.Domain.Messaging.MessageReads;
using zavit.Domain.Messaging.Messages;
using zavit.Domain.Messaging.MessageThreads;

namespace zavit.Infrastructure.Ioc.DomainInstallers
{
    public class MessagingInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IMessageService>().ImplementedBy<MessageService>().LifestyleTransient(),
                Component.For<IMessageThreadService>().ImplementedBy<MessageThreadService>().LifestyleTransient(),
                Component.For<INewMessageThreadProvider>().ImplementedBy<NewMessageThreadProvider>().LifestyleTransient(),
                Component.For<INewMessageProvider>().ImplementedBy<NewMessageProvider>().LifestyleTransient(),
                Component.For<IMessageReadService>().ImplementedBy<MessageReadService>().LifestyleTransient(),
                Component.For<IMessageReadCreator>().ImplementedBy<MessageReadCreator>().LifestyleTransient(),
                Component.For<IMessageReadProcessor>().ImplementedBy<MessageReadProcessor>().LifestyleTransient(),
                Component.For<IMessageReadObserver>().ImplementedBy<NoActionMessageReadObserver>().LifestyleSingleton()
                );
        }
    }
}