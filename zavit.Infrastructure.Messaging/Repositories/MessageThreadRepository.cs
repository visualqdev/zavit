using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using NHibernate.Transform;
using zavit.Domain.Accounts;
using zavit.Domain.Messaging;
using zavit.Domain.Messaging.MessageReads;
using zavit.Domain.Messaging.Messages;
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

        public IMessageInbox GetInbox(int accountId)
        {
            Account participantAlias = null;

            var messageThreadIds = QueryOver.Of<MessageThread>()
                .JoinAlias(t => t.Participants, () => participantAlias, JoinType.InnerJoin)
                .Where(() => participantAlias.Id == accountId)
                .Select(m => m.Id);

            Account threadParticipantAlias = null;
            var messageThreads = _session.QueryOver<MessageThread>()
                .JoinAlias(t => t.Participants, () => threadParticipantAlias, JoinType.InnerJoin)
                .Fetch(t => t.Participants).Eager
                .WithSubquery.WhereProperty(t => t.Id).In(messageThreadIds)
                .TransformUsing(Transformers.DistinctRootEntity)
                .OrderBy(t => t.CreatedOn).Desc
                .Future();

            
            Message messageAlias = null;
            MessageRead messageReadAlias = null;

            var unreadMessageIds = _session.QueryOver(() => messageReadAlias)
                .JoinAlias(r => r.Message, () => messageAlias, JoinType.InnerJoin)
                .WithSubquery.WhereProperty(() => messageAlias.MessageThread.Id).In(messageThreadIds)
                .And(() => !messageReadAlias.HasRead)
                .And(() => messageReadAlias.Account.Id == accountId)
                .SelectList(list => list
                    .SelectGroup(() => messageAlias.MessageThread.Id)
                    .SelectCount(() => messageAlias.Id))
                .Future<object[]>();

            MessageThread threadAlias = null;

            var latestThreadMessageIdsFuture = _session.QueryOver(() => threadAlias)
                .WithSubquery.WhereProperty(() => threadAlias.Id).In(messageThreadIds)
                .SelectList(list => list
                    .Select(() => threadAlias.Id)
                    .SelectSubQuery(
                        QueryOver.Of<Message>()
                            .Where(m => m.MessageThread.Id == threadAlias.Id)
                            .OrderBy(m => m.SentOn).Desc
                            .Select(m => m.Id)
                            .Take(1)))
                .Future<object[]>();

            var latestThreadMessageIds = latestThreadMessageIdsFuture.Where(v => v[1] != null).ToDictionary(k => (int)k[0], v => (int)v[1]);

            var latestMessages = _session.QueryOver<Message>()
                .WhereRestrictionOn(m => m.Id).IsIn(latestThreadMessageIds.Values)
                .Fetch(m => m.Sender).Eager
                .List<Message>();


            return new MessageInbox
            {
                AccountId = accountId,
                Threads = messageThreads,
                UnreadMessageCountsPerThread = unreadMessageIds.ToDictionary(k => (int)k[0], v => (int)v[1]),
                LatestMessagesPerThread = latestMessages.ToDictionary(k => k.MessageThread.Id, v => v)
            };
        }
    }
}