using zavit.Domain.Accounts;
using zavit.Web.Api.Dtos.MessageRecipients;
using zavit.Web.Api.DtoServices.Profiles;

namespace zavit.Web.Api.DtoFactories.MessageRecipients
{
    public class MessageRecipientDtoFactory : IMessageRecipientDtoFactory
    {
        readonly IProfileImageUrlBuilder _profileImageUrlBuilder;

        public MessageRecipientDtoFactory(IProfileImageUrlBuilder profileImageUrlBuilder)
        {
            _profileImageUrlBuilder = profileImageUrlBuilder;
        }

        public MessageRecipientDto CreateItem(Account account)
        {
            return new MessageRecipientDto
            {
                DisplayName = account.Profile.DisplayName,
                AccountId = account.Id,
                ProfileImageUrl = _profileImageUrlBuilder.Build(account.Profile)
            };
        }
    }
}