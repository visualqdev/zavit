using zavit.Domain.Messaging.Messages;
using zavit.Web.Api.Dtos.Messaging.Messages;

namespace zavit.Web.Api.DtoServices.Messaging.MessageThreads.NewMessages
{
    public interface INewMessageRequestProvider
    {
        NewMessageRequest Provide(MessageDto messageDto);
    }
}