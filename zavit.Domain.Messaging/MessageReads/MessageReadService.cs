using System.Collections.Generic;
using System.Linq;
using zavit.Domain.Accounts;
using zavit.Domain.Shared;

namespace zavit.Domain.Messaging.MessageReads
{
    public class MessageReadService : IMessageReadService
    {
        readonly IDateTime _dateTime;
        readonly IMessageReadRepository _messageReadRepository;
        readonly IMessageReadProcessor _messageReadProcessor;
        readonly IEnumerable<IMessageReadObserver> _messageReadObservers;

        public MessageReadService(IDateTime dateTime, IMessageReadRepository messageReadRepository, IMessageReadProcessor messageReadProcessor, IEnumerable<IMessageReadObserver> messageReadObservers)
        {
            _dateTime = dateTime;
            _messageReadRepository = messageReadRepository;
            _messageReadProcessor = messageReadProcessor;
            _messageReadObservers = messageReadObservers;
        }

        public void MessagesRead(int messageThreadId, Account account)
        {
            var dateRead = _dateTime.UtcNow;
            var unreadMessages = _messageReadRepository.UnreadMessagesByUser(messageThreadId, account.Id, dateRead);
            _messageReadProcessor.Process(unreadMessages, account, dateRead);

            var completelyReadMessages = _messageReadRepository.GetReadMessageIds(messageThreadId, unreadMessages.Select(m => m.Id));
            foreach (var messageReadObserver in _messageReadObservers)
            {
                messageReadObserver.MessagesRead(completelyReadMessages, messageThreadId);
            }
        }
    }
}