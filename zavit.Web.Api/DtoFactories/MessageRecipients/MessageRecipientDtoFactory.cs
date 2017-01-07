using zavit.Domain.Accounts;
using zavit.Web.Api.Dtos.MessageRecipients;

namespace zavit.Web.Api.DtoFactories.MessageRecipients
{
    public class MessageRecipientDtoFactory : IMessageRecipientDtoFactory
    {
        public MessageRecipientDto CreateItem(Account account)
        {
            return new MessageRecipientDto
            {
                DisplayName = account.Profile.DisplayName,
                AccountId = account.Id
            };
        }
    }
}