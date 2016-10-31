using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using zavit.Domain.Messaging.MessageReads;
using zavit.Domain.Messaging.Messages;

namespace zavit.Infrastructure.Messaging.Repositories
{
    public class MessageReadRepository : IMessageReadRepository
    {
        readonly ISession _session;

        public MessageReadRepository(ISession session)
        {
            _session = session;
        }

        public IList<int> GetReadMessageIds(IEnumerable<int> messageIds)
        {
            var messageIdsArray = messageIds.ToArray();
            if (messageIdsArray.Length == 0)
                return new List<int>();

            Message messageAlias = null;

            return _session.QueryOver<MessageRead>()
                .JoinAlias(r => r.Message, () => messageAlias, JoinType.InnerJoin)
                .WhereRestrictionOn(() => messageAlias.Id).IsIn(messageIdsArray)
                .WithSubquery.WhereProperty(r => r.Message.Id).NotIn(
                    QueryOver.Of<MessageRead>()
                        .WhereRestrictionOn(r => r.Message.Id).IsIn(messageIdsArray)
                        .And(r => !r.HasRead)
                        .Select(r => r.Message.Id)
                )
                .Select(Projections.Distinct(Projections.Property(() => messageAlias.Id)))
                .List<int>();
        }

        public void Save(IEnumerable<MessageRead> instantMessageReads)
        {
            foreach (var instantMessageRead in instantMessageReads)
            {
                _session.Save(instantMessageRead);
            }
            _session.Flush();
        }

        public void Update(IEnumerable<MessageRead> messageReads)
        {
            foreach (var instantMessageRead in messageReads)
            {
                _session.Update(instantMessageRead);
            }
            _session.Flush();
        }

        public IList<MessageRead> GetPendingMessageReads(int messageThreadId, int accountId)
        {
            Message messageAlias = null;

            return _session.QueryOver<MessageRead>()
                .JoinAlias(r => r.Message, () => messageAlias, JoinType.InnerJoin)
                .Where(() => messageAlias.MessageThread.Id == messageThreadId)
                .And(r => r.Account.Id == accountId)
                .And(r => !r.HasRead)
                .List();
        }
    }
}