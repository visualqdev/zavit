using zavit.Domain.Accounts;
using zavit.Domain.Shared.ResultCollections;
using zavit.Web.Api.Dtos.MessageRecipients;

namespace zavit.Web.Api.DtoFactories.MessageRecipients
{
    public interface IMessageRecipientCollectionDtoFactory
    {
        MessageRecipientCollectionDto CreateItem(IResultCollection<Account> recipientResultCollection);
    }
}