using zavit.Domain.Messaging.Messages;
using zavit.Domain.Shared.ResultCollections;
using zavit.Web.Api.Dtos.Messaging.Messages;

namespace zavit.Web.Api.DtoFactories.Messaging.Messages
{
    public interface IMessageCollectionDtoFactory
    {
        MessagesCollectionDto CreateItem(IResultCollection<Message> messageCollection);
    }
}