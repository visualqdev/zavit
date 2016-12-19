using zavit.Domain.Accounts;
using zavit.Domain.Shared.ResultCollections;

namespace zavit.Domain.Messaging.Recipients
{
    public interface IMessageRecipientService
    {
        IResultCollection<Account> SuggestRecipients(string searchTerm, int skip, int take, Account account);
    }
}