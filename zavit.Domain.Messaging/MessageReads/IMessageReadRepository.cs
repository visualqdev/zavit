﻿using System;
using System.Collections.Generic;
using zavit.Domain.Messaging.Messages;

namespace zavit.Domain.Messaging.MessageReads
{
    public interface IMessageReadRepository
    {
        IList<Message> GetReadMessages(IEnumerable<int> messageIds);
        void Save(IEnumerable<MessageRead> instantMessageReads);
        void Update(IEnumerable<MessageRead> messageReads);
        IList<MessageRead> GetPendingMessageReads(int messageThreadId, int accountId);
    }
}