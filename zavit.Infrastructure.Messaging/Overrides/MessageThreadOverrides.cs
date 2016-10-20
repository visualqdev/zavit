using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using zavit.Domain.Messaging;

namespace zavit.Infrastructure.Messaging.Overrides
{
    public class MessageThreadOverrides : IAutoMappingOverride<MessageThread>
    {
        public void Override(AutoMapping<MessageThread> mapping)
        {
            mapping.HasManyToMany(t => t.Participants).Table("MessageThreadParticipant");
        }
    }
}