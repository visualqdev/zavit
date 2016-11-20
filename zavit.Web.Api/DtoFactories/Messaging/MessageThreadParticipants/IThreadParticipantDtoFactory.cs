using zavit.Domain.Accounts;
using zavit.Web.Api.Dtos.Messaging.MessageThreads;

namespace zavit.Web.Api.DtoFactories.Messaging.MessageThreadParticipants
{
    public interface IThreadParticipantDtoFactory
    {
        ThreadParticipantDto CreateItem(Account participant);
    }
}