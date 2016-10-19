using zavit.Web.Api.Dtos.Messaging.Messages;

namespace zavit.Web.Api.DtoServices.Messaging.Messages
{
    public interface IMessageDtoService
    {
        MessageDto SendMessage(int messageThreadId, MessageDto messageDto);
        MessagesCollectionDto GetMessages(int messageThreadId, int? olderThanMessageId, int take);
    }
}