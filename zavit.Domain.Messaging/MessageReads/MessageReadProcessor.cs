using System;
using System.Collections.Generic;
using System.Linq;
using zavit.Domain.Accounts;
using zavit.Domain.Messaging.Messages;

namespace zavit.Domain.Messaging.MessageReads
{
    public class MessageReadProcessor : IMessageReadProcessor
    {
        readonly IMessageReadCreator _instantMessageReadCreator;
        readonly IMessageReadRepository _instantMessageReadRepository;

        public MessageReadProcessor(IMessageReadCreator instantMessageReadCreator, IMessageReadRepository instantMessageReadRepository)
        {
            _instantMessageReadCreator = instantMessageReadCreator;
            _instantMessageReadRepository = instantMessageReadRepository;
        }

        public void Process(IList<Message> unreadMessages, Account account, DateTime dateRead)
        {
            var instantMessageReads = unreadMessages
                .Where(m => m.Sender.Id != account.Id)
                .Select(m => _instantMessageReadCreator.Create(account, m, dateRead));

            _instantMessageReadRepository.Save(instantMessageReads);
        }
    }
}