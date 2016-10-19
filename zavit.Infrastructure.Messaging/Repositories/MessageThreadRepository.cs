using NHibernate;
using NHibernate.SqlCommand;
using NHibernate.Transform;
using zavit.Domain.Accounts;
using zavit.Domain.Messaging;
using zavit.Domain.Messaging.MessageThreads;

namespace zavit.Infrastructure.Messaging.Repositories
{
    public class MessageThreadRepository : IMessageThreadRepository
    {
        readonly ISession _session;

        public MessageThreadRepository(ISession session)
        {
            _session = session;
        }

        public void Save(MessageThread messageThread)
        {
            _session.Save(messageThread);
            _session.Flush();
        }

        public bool CanUserAccessThread(int accountId, int? messageThreadId)
        {
            Account accountAlias = null;

            var count = _session.QueryOver<MessageThread>()
                .JoinAlias(g => g.Participants, () => accountAlias, JoinType.InnerJoin)
                .Where(t => t.Id == messageThreadId)
                .And(() => accountAlias.Id == accountId)
                .RowCount();

            return count > 0;
        }

        public MessageThread GetMessageThread(int messageThreadId)
        {
            Account accountAlias = null;

            return _session.QueryOver<MessageThread>()
                .JoinAlias(g => g.Participants, () => accountAlias, JoinType.InnerJoin)
                .Fetch(t => t.Participants).Eager
                .Where(t => t.Id == messageThreadId)
                .TransformUsing(Transformers.DistinctRootEntity)
                .SingleOrDefault();
        }
    }
}