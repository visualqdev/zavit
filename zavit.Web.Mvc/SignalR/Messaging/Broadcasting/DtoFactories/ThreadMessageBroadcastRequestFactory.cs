using System.Linq;
using zavit.Domain.Messaging.Messages;
using zavit.Web.Api.DtoFactories.Messaging.Messages;
using zavit.Web.Api.Dtos.Messaging.Messages;
using zavit.Web.Mvc.SignalR.ConnectionIds;
using zavit.Web.Mvc.SignalR.Hubs;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting.Dtos;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting.GroupIds;

namespace zavit.Web.Mvc.SignalR.Messaging.Broadcasting.DtoFactories
{
    public class ThreadMessageBroadcastRequestFactory : IThreadMessageBroadcastRequestFactory
    {
        readonly IMessageDtoFactory _messageDtoFactory;
        readonly IConnectionIdProvider _connectionIdProvider;
        readonly IThreadGroupIdProvider _threadGroupIdProvider;

        public ThreadMessageBroadcastRequestFactory(IMessageDtoFactory messageDtoFactory, IConnectionIdProvider connectionIdProvider, IThreadGroupIdProvider threadGroupIdProvider)
        {
            _messageDtoFactory = messageDtoFactory;
            _connectionIdProvider = connectionIdProvider;
            _threadGroupIdProvider = threadGroupIdProvider;
        }

        public BroadcastRequest<MessageDto> CreateItem(Message message)
        {
            var messageDto = _messageDtoFactory.CreateItem(message);
            var groupIds = message.GetRecipients().Select(r => _threadGroupIdProvider.Provide(message.MessageThread.Id, r.Id)).ToList();

            return new BroadcastRequest<MessageDto>
            {
                Dto = messageDto,
                GroupIds = groupIds,
                ConnectionIdsToExclude = new[] { _connectionIdProvider.GetConnectionId() }
            };
        }
    }
}