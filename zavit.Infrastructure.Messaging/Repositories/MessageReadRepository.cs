using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using zavit.Domain.Accounts;
using zavit.Domain.Messaging;
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

        public IList<Message> UnreadMessagesByUser(int messageThreadId, int accountId, DateTime dateRead)
        {
            MessageRead messageReadAlias = null;
            MessageThread messageThreadAlias = null;
            Message messageAlias = null;
            Account accountAlias = null;

            var unreadMessageIds = QueryOver.Of(() => messageReadAlias)
                .JoinAlias(r => r.Message, () => messageAlias, JoinType.RightOuterJoin, Restrictions.On(() => messageReadAlias.Account.Id).IsIn(new[] { accountId }))
                .JoinAlias(() => messageAlias.MessageThread, () => messageThreadAlias, JoinType.InnerJoin)
                .JoinAlias(() => messageThreadAlias.Participants, () => accountAlias, JoinType.InnerJoin)
                .Where(() => messageThreadAlias.Id == messageThreadId)
                .And(() => accountAlias.Id == accountId)
                .And(Restrictions.On(() => messageReadAlias.Message).IsNull)
                .And(() => messageAlias.Sender.Id != accountId)
                .Select(Projections.ProjectionList()
                        .Add(Projections.Property(() => messageAlias.Id)));

            return _session.QueryOver<Message>()
                .WithSubquery.WhereProperty(x => x.Id).In(unreadMessageIds).List();
        }

        public IList<int> GetReadMessageIds(int messageThreadId, IEnumerable<int> messageIds)
        {
            var messageIdsArray = messageIds.ToArray();
            if (messageIdsArray.Length == 0)
                return new List<int>();

            Account accountAlias = null;
            var userCount = _session.QueryOver<MessageThread>()
                .JoinAlias(m => m.Participants, () => accountAlias, JoinType.InnerJoin)
                .Where(m => m.Id == messageThreadId)
                .RowCount();

            var readMessages = _session.QueryOver<MessageRead>()
                .SelectList(list => list
                    .SelectGroup(r => r.Message.Id)
                    .SelectCount(r => r.Account.Id)
                )
                .Select(Projections.GroupProperty(Projections.Property<MessageRead>(r => r.Message.Id)),
                        Projections.Count<MessageRead>(r => r.Account.Id))
                .Where(Restrictions.Ge(Projections.Count<MessageRead>(r => r.Account.Id), userCount - 1))
                .AndRestrictionOn(r => r.Message.Id).IsIn(messageIdsArray)
                .List<object[]>();

            return readMessages.Select(r => (int)r[0]).ToList();
        }

        public void Save(IEnumerable<MessageRead> instantMessageReads)
        {
            foreach (var instantMessageRead in instantMessageReads)
            {
                _session.Save(instantMessageRead);
            }
            _session.Flush();
        }
    }
}