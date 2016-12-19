using System.Linq;
using zavit.Domain.Messaging.Messages;
using zavit.Web.Api.DtoFactories.Messaging.Messages;
using zavit.Web.Api.Dtos.Messaging.Messages;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting.Dtos;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting.GroupIds;

namespace zavit.Web.Mvc.SignalR.Messaging.Broadcasting.DtoFactories
{
    public class InboxMessageBroadcastRequestFactory : IInboxMessageBroadcastRequestFactory
    {
        readonly IMessageDtoFactory _messageDtoFactory;
        readonly IInboxGroupIdProvider _inboxGroupIdProvider;

        public InboxMessageBroadcastRequestFactory(IMessageDtoFactory messageDtoFactory, IInboxGroupIdProvider inboxGroupIdProvider)
        {
            _messageDtoFactory = messageDtoFactory;
            _inboxGroupIdProvider = inboxGroupIdProvider;
        }

        public BroadcastRequest<MessageDto> CreateItem(Message message)
        {
            var messageDto = _messageDtoFactory.CreateItem(message);
            var groupIds = message.GetRecipients().Select(r => _inboxGroupIdProvider.Provide(r.Id)).ToList();
            groupIds.Add(_inboxGroupIdProvider.Provide(message.Sender.Id));

            return new BroadcastRequest<MessageDto>
            {
                Dto = messageDto,
                GroupIds = groupIds,
                ConnectionIdsToExclude = new string[0]
            };
        }
    }
}