using NHibernate;
using NHibernate.Criterion;
using zavit.Domain.Messaging.Messages;
using zavit.Domain.Shared.ResultCollections;
using zavit.Infrastructure.Core.ResultCollections;

namespace zavit.Infrastructure.Messaging.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        readonly ISession _session;

        public MessageRepository(ISession session)
        {
            _session = session;
        }

        public void Save(Message message)
        {
            _session.Save(message);
            _session.Flush();
        }

        public IResultCollection<Message> GetMessages(int messageThreadId, int? olderThanMessageId, int take)
        {
            var messagesOnThread =_session.QueryOver<Message>()
                .Where(m => m.MessageThread.Id == messageThreadId);

            if (olderThanMessageId.HasValue)
            {
                var startingMessageDate = QueryOver.Of<Message>()
                .Where(m => m.Id == olderThanMessageId.Value)
                .Select(m => m.SentOn)
                .Take(1);

                messagesOnThread.And(Restrictions.Disjunction()
                    .Add(Subqueries.WhereProperty<Message>(m => m.SentOn).Lt(startingMessageDate))
                    .Add(Restrictions.Conjunction()
                    .Add(Subqueries.WhereProperty<Message>(m => m.SentOn).Eq(startingMessageDate))
                    .Add(Restrictions.Where<Message>(m => m.Id < olderThanMessageId.Value))));
            }

            var instantMessages = messagesOnThread
                .OrderBy(m => m.SentOn).Desc
                .OrderBy(m => m.Id).Desc
                .Take(take + 1)
                .List<Message>();

            return new ResultCollection<Message>(instantMessages, take);
        }
    }
}