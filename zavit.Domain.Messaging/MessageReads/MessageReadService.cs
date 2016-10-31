using System.Collections.Generic;
using System.Linq;
using zavit.Domain.Accounts;
using zavit.Domain.Messaging.Messages;
using zavit.Domain.Shared;

namespace zavit.Domain.Messaging.MessageReads
{
    public class MessageReadService : IMessageReadService
    {
        readonly IDateTime _dateTime;
        readonly IMessageReadRepository _messageReadRepository;
        readonly IEnumerable<IMessageReadObserver> _messageReadObservers;
        readonly IMessageReadCreator _messageReadCreator;

        public MessageReadService(IDateTime dateTime, IMessageReadRepository messageReadRepository, IEnumerable<IMessageReadObserver> messageReadObservers, IMessageReadCreator messageReadCreator)
        {
            _dateTime = dateTime;
            _messageReadRepository = messageReadRepository;
            _messageReadObservers = messageReadObservers;
            _messageReadCreator = messageReadCreator;
        }

        public void MessagesRead(int messageThreadId, Account account)
        {
            var pendingReads = _messageReadRepository.GetPendingMessageReads(messageThreadId, account.Id);
            var currentDate = _dateTime.UtcNow;
            foreach (var pendingRead in pendingReads)
            {
                pendingRead.UserHasRead(currentDate);
            }

            _messageReadRepository.Update(pendingReads);

            var completelyReadMessages = _messageReadRepository.GetReadMessageIds(pendingReads.Select(m => m.Message.Id));
            foreach (var messageReadObserver in _messageReadObservers)
            {
                messageReadObserver.MessagesRead(completelyReadMessages, messageThreadId);
            }
        }

        public void MessageSent(Message message)
        {
            var messageReads = message
                .GetRecipients()
                .Select(recipient => _messageReadCreator.Create(recipient, message))
                .ToList();

            _messageReadRepository.Save(messageReads);
        }
    }
}