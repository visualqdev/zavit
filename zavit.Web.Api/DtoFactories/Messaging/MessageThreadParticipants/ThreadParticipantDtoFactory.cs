using zavit.Domain.Accounts;
using zavit.Web.Api.Dtos.Messaging.MessageThreads;

namespace zavit.Web.Api.DtoFactories.Messaging.MessageThreadParticipants
{
    public class ThreadParticipantDtoFactory : IThreadParticipantDtoFactory
    {
        public ThreadParticipantDto CreateItem(Account participant)
        {
            return new ThreadParticipantDto
            {
                Id = participant.Id,
                DisplayName = participant.DisplayName
            };
        }
    }
}