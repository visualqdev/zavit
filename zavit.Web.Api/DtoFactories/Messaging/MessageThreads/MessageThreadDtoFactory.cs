using System.Linq;
using zavit.Domain.Messaging;
using zavit.Web.Api.DtoFactories.Messaging.MessageThreadParticipants;
using zavit.Web.Api.Dtos.Messaging.MessageThreads;

namespace zavit.Web.Api.DtoFactories.Messaging.MessageThreads
{
    public class MessageThreadDtoFactory : IMessageThreadDtoFactory
    {
        readonly IThreadParticipantDtoFactory _threadParticipantDtoFactory;

        public MessageThreadDtoFactory(IThreadParticipantDtoFactory threadParticipantDtoFactory)
        {
            _threadParticipantDtoFactory = threadParticipantDtoFactory;
        }

        public MessageThreadDto CreateItem(MessageThread messageThread)
        {
            return new MessageThreadDto
            {
                ThreadId = messageThread.Id,
                Participants = messageThread.Participants.Select(p => _threadParticipantDtoFactory.CreateItem(p))
            };
        }
    }
}