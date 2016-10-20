using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using zavit.Domain.Accounts;
using zavit.Domain.Messaging;
using zavit.Domain.Messaging.MessageReads;
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

        public IResultCollection<MessageInfo> GetMessages(int messageThreadId, int? olderThanMessageId, int take)
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
                .AndRestrictionOn(r => r.Message.Id).IsIn(instantMessages.Select(m => m.Id).ToArray())
                .List<object[]>();

            return new ResultCollection<MessageInfo>(
                instantMessages.Select(m => new MessageInfo
                {
                    Message = m,
                    HasBeenRead = readMessages.Any(r => (int)r[0] == m.Id)
                }), 
                take);
        }
    }
}