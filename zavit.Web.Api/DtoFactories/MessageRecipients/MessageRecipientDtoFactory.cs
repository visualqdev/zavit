using zavit.Domain.Accounts;
using zavit.Domain.Profiles.ProfileImages;
using zavit.Web.Api.Dtos.MessageRecipients;
using zavit.Web.Api.DtoServices.Profiles;

namespace zavit.Web.Api.DtoFactories.MessageRecipients
{
    public class MessageRecipientDtoFactory : IMessageRecipientDtoFactory
    {
        readonly IProfileImageStorage _profileImageStorage;

        public MessageRecipientDtoFactory(IProfileImageStorage profileImageStorage)
        {
            _profileImageStorage = profileImageStorage;
        }

        public MessageRecipientDto CreateItem(Account account)
        {
            return new MessageRecipientDto
            {
                DisplayName = account.Profile.DisplayName,
                AccountId = account.Id,
                ProfileImageUrl = _profileImageStorage.ImageUrl(account.Profile.ProfileImage)
            };
        }
    }
}