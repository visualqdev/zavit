using System;
using System.Collections.Generic;
using zavit.Domain.Messaging.Messages;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting.Dtos;

namespace zavit.Web.Mvc.SignalR.Messaging.Broadcasting.DtoFactories
{
    public interface IReadMessagesBroadcastDtoFactory
    {
        BroadcastRequest<ReadMessagesDto> CreateItem(IList<Message> completelyReadMessages, int messageThreadId, int senderId);
    }
}