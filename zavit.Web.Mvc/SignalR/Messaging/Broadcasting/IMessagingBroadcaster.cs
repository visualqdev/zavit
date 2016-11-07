using zavit.Web.Api.Dtos.Messaging.Messages;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting.Dtos;

namespace zavit.Web.Mvc.SignalR.Messaging.Broadcasting
{
    public interface IMessagingBroadcaster
    {
        void ThreadMessageSent(BroadcastRequest<MessageDto> messageBroadcastRequest);
        void ThreadMessageRead(BroadcastRequest<ReadMessagesDto> readMessagesBroadcastRequest);
    }
}