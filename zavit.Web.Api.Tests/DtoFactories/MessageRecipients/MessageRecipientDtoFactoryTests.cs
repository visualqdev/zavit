using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Profiles;
using zavit.Domain.Profiles.ProfileImages;
using zavit.Web.Api.DtoFactories.MessageRecipients;
using zavit.Web.Api.Dtos.MessageRecipients;
using zavit.Web.Api.DtoServices.Profiles;

namespace zavit.Web.Api.Tests.DtoFactories.MessageRecipients 
{
    [Subject("MessageRecipientDtoFactory")]
    public class MessageRecipientDtoFactoryTests : TestOf<MessageRecipientDtoFactory>
    {
        class When_creating_message_recipient_dto
        {
            Because of = () => _result = Subject.CreateItem(_account);

            It should_set_the_display_name_to_be_the_same_as_account = 
                () => _result.DisplayName.ShouldEqual(_account.Profile.DisplayName);

            It should_set_the_account_id_to_be_the_same_as_account =
                () => _result.AccountId.ShouldEqual(_account.Id);

            It should_set_the_profile_image_url_to_be_the_url_provided_by_the_builder =
                () => _result.ProfileImageUrl.ShouldEqual(ProfileUrl);

            Establish context = () =>
            {
                _account = NewInstanceOf<Account>();
                _account.Id = 123;
                _account.Profile = NewInstanceOf<Profile>();
                _account.Profile.DisplayName = "Test display name";
                _account.Profile.ProfileImage = "ProfileImage";

                Injected<IProfileImageStorage>().Stub(b => b.ImageUrl(_account.Profile.ProfileImage)).Return(ProfileUrl);
            };

            static Account _account;
            static MessageRecipientDto _result;
            const string ProfileUrl = "/profile/image/url";
        }
    }
}

