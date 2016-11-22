using zavit.Domain.Messaging.Messages;
using zavit.Web.Api.Dtos.Messaging.Messages;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting.Dtos;

namespace zavit.Web.Mvc.SignalR.Messaging.Broadcasting.DtoFactories
{
    public interface IInboxMessageBroadcastRequestFactory
    {
        BroadcastRequest<MessageDto> CreateItem(Message message);
    }
}