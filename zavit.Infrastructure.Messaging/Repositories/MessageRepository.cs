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
                .Fetch(m => m.Sender).Eager
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

            var messageIdsArray = instantMessages.Select(m => m.Id).ToArray();
            var readMessages = _session.QueryOver<MessageRead>()
                .WhereRestrictionOn(r => r.Message.Id).IsIn(messageIdsArray)
                .WithSubquery.WhereProperty(r => r.Message.Id).NotIn(
                    QueryOver.Of<MessageRead>()
                        .WhereRestrictionOn(r => r.Message.Id).IsIn(messageIdsArray)
                        .And(r => !r.HasRead)
                        .Select(r => r.Message.Id)
                )
                .Select(r => r.Message.Id)
                .List<int>();

            return new ResultCollection<MessageInfo>(
                instantMessages.Select(m => new MessageInfo
                {
                    Message = m,
                    Status = readMessages.Any(r => r == m.Id) ? MessageStatus.Read : MessageStatus.Sent
                }), 
                take);
        }
    }
}