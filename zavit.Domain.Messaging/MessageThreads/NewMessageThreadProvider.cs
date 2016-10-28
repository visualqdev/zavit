using zavit.Domain.Accounts;
using zavit.Domain.Shared;

namespace zavit.Domain.Messaging.MessageThreads
{
    public class NewMessageThreadProvider : INewMessageThreadProvider
    {
        readonly IDateTime _dateTime;
        readonly IAccountRepository _accountRepository;

        public NewMessageThreadProvider(IDateTime dateTime, IAccountRepository accountRepository)
        {
            _dateTime = dateTime;
            _accountRepository = accountRepository;
        }

        public MessageThread Provide(NewMessageThreadRequest newMessageThreadRequest)
        {
            var participants = _accountRepository.GetAccounts(newMessageThreadRequest.ParticipantIds);
            var currentDateTime = _dateTime.UtcNow;

            return new MessageThread
            {
                CreatedOn = currentDateTime,
                LastUpdatedOn = currentDateTime,
                Participants = participants
            };
        }
    }
}