using System.Collections.Generic;

namespace zavit.Domain.Messaging.MessageReads
{
    public interface IMessageReadObserver
    {
        void MessagesRead(IList<int> completelyReadMessages, int messageThreadId);
    }

    public class NoActionMessageReadObserver : IMessageReadObserver
    {
        public void MessagesRead(IList<int> completelyReadMessages, int messageThreadId)
        {
        }
    }
}