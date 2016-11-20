using System.Collections.Generic;
using zavit.Domain.Messaging.Messages;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting.Dtos;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting.GroupIds;

namespace zavit.Web.Mvc.SignalR.Messaging.Broadcasting.DtoFactories
{
    public class ReadMessagesBroadcastDtoFactory : IReadMessagesBroadcastDtoFactory
    {
        readonly IThreadGroupIdProvider _threadGroupIdProvider;
        readonly IReadMessagesDtoFactory _readMessagesDtoFactory;

        public ReadMessagesBroadcastDtoFactory(IThreadGroupIdProvider threadGroupIdProvider, IReadMessagesDtoFactory readMessagesDtoFactory)
        {
            _threadGroupIdProvider = threadGroupIdProvider;
            _readMessagesDtoFactory = readMessagesDtoFactory;
        }

        public BroadcastRequest<ReadMessagesDto> CreateItem(IList<Message> completelyReadMessages, int messageThreadId, int senderId)
        {
            return new BroadcastRequest<ReadMessagesDto>
            {
                ConnectionIdsToExclude = new string[0],
                GroupIds = new List<string> { _threadGroupIdProvider.Provide(messageThreadId, senderId) },
                Dto = _readMessagesDtoFactory.CreateItem(completelyReadMessages)
            };
        }
    }
}