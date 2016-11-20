using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using zavit.Domain.Messaging.MessageReads;
using zavit.Domain.Messaging.Messages;
using zavit.Domain.Messaging.MessageThreads;
using zavit.Infrastructure.Messaging.Repositories;

namespace zavit.Infrastructure.Ioc.Infrastructure
{
    public class InfrastructureMessagingInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IMessageRepository>().ImplementedBy<MessageRepository>().LifestyleTransient(),
                Component.For<IMessageReadRepository>().ImplementedBy<MessageReadRepository>().LifestyleTransient(),
                Component.For<IMessageThreadRepository>().ImplementedBy<MessageThreadRepository>().LifestyleTransient()
                );
        }
    }
}
