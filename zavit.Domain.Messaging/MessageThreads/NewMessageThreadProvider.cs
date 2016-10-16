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

            return new MessageThread
            {
                CreatedOn = _dateTime.UtcNow,
                Participants = participants
            };
        }
    }
}