using NHibernate;
using zavit.Domain.Messaging.Messages;

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
    }
}