using System.Collections.Generic;
using System.Linq;
using zavit.Domain.Messaging.Messages;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting.Dtos;

namespace zavit.Web.Mvc.SignalR.Messaging.Broadcasting.DtoFactories
{
    public class ReadMessagesBroadcastRequestsProvider : IReadMessagesBroadcastRequestsProvider
    {
        readonly IReadMessagesBroadcastDtoFactory _readMessagesBroadcastDtoFactory;

        public ReadMessagesBroadcastRequestsProvider(IReadMessagesBroadcastDtoFactory readMessagesBroadcastDtoFactory)
        {
            _readMessagesBroadcastDtoFactory = readMessagesBroadcastDtoFactory;
        }

        public IEnumerable<BroadcastRequest<ReadMessagesDto>> Provide(IList<Message> completelyReadMessages, int messageThreadId)
        {
            var messagesGroupedBySender = completelyReadMessages.GroupBy(k => k.Sender.Id);

            return messagesGroupedBySender.Select(messages => _readMessagesBroadcastDtoFactory.CreateItem(messages.ToList(), messageThreadId, messages.Key));
        }
    }
}