using zavit.Domain.Accounts;
using zavit.Web.Api.Dtos.MessageRecipients;

namespace zavit.Web.Api.DtoFactories.MessageRecipients
{
    public interface IMessageRecipientDtoFactory
    {
        MessageRecipientDto CreateItem(Account account);
    }
}