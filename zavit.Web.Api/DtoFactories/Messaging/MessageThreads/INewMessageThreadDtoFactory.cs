using zavit.Domain.Messaging;
using zavit.Domain.Messaging.Messages;
using zavit.Web.Api.Dtos.Messaging.MessageThreads;

namespace zavit.Web.Api.DtoFactories.Messaging.MessageThreads
{
    public interface INewMessageThreadDtoFactory
    {
        NewMessageThreadDto CreateItem(MessageThread messageThread, Message message);
    }
}