using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Profiles.Registration;

namespace zavit.Domain.Profiles.Tests.Registration 
{
    [Subject("ProfileCreator")]
    public class ProfileCreatorTests : TestOf<ProfileCreator>
    {
        class When_creating_profile_from_account
        {
            Because of = () => _result = Subject.CreateProfile(_account, _accountProfileRegistration);

            It should_set_the_account_to_the_provided_account = () => _result.Account.ShouldEqual(_account);

            It should_set_the_gender_to_value_not_specified = () => _result.Gender.ShouldEqual(_accountProfileRegistration.Gender);

            It should_set_the_profile_image_tp_the_new_profile_image_instance =
                () => _result.ProfileImage.ShouldEqual(_profileImage);

            Establish context = () =>
            {
                _account = NewInstanceOf<Account>();
                _accountProfileRegistration = NewInstanceOf<IAccountProfileRegistration>();
                _accountProfileRegistration.Stub(r => r.Gender).Return(Gender.Female);

                _profileImage = NewInstanceOf<ProfileImage>();
                Injected<IProfileImageCreator>().Stub(c => c.Create(_accountProfileRegistration)).Return(_profileImage);
            };

            static Account _account;
            static Profile _result;
            static IAccountProfileRegistration _accountProfileRegistration;
            static ProfileImage _profileImage;
        }
    }
}

