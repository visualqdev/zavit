using System.Collections.Generic;
using zavit.Domain.Messaging.Messages;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting.Dtos;

namespace zavit.Web.Mvc.SignalR.Messaging.Broadcasting.DtoFactories
{
    public interface IReadMessagesBroadcastRequestsProvider
    {
        IEnumerable<BroadcastRequest<ReadMessagesDto>> Provide(IList<Message> completelyReadMessages, int messageThreadId);
    }
}