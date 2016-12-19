using zavit.Domain.Accounts;
using zavit.Domain.Shared.ResultCollections;

namespace zavit.Domain.Messaging.Recipients
{
    public class MessageRecipientService : IMessageRecipientService
    {
        readonly IAccountRepository _accountRepository;

        public MessageRecipientService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public IResultCollection<Account> SuggestRecipients(string searchTerm, int skip, int take, Account account)
        {
            var resultCollection =_accountRepository.Search(searchTerm, skip, take, account.Id);
            return resultCollection;
        }
    }
}