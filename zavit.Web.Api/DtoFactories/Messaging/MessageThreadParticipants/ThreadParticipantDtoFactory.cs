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
                AccountId = participant.Id,
                DisplayName = participant.Profile.DisplayName
            };
        }
    }
}