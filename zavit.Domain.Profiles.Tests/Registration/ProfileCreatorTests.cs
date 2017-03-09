using System.IO;
using System.Threading.Tasks;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Profiles.ProfileImages;
using zavit.Domain.Profiles.Registration;

namespace zavit.Domain.Profiles.Tests.Registration 
{
    [Subject("ProfileCreator")]
    public class ProfileCreatorTests : TestOf<ProfileCreator>
    {
        class When_creating_profile_from_account
        {
            Because of = () => _result = Subject.CreateProfile(_accountProfileRegistration).Result;

            It should_set_the_gender_to_value_not_specified = () => _result.Gender.ShouldEqual(_accountProfileRegistration.Gender);

            It should_set_the_profile_image_to_the_new_profile_image_instance =
                () => _result.ProfileImage.ShouldEqual(ProfileImage);

            It should_set_the_display_name_to_be_the_same_as_registration_display_name =
                () => _result.DisplayName.ShouldEqual(_accountProfileRegistration.DisplayName);

            It should_set_the_email_to_be_the_same_as_registration_email =
                () => _result.Email.ShouldEqual(_accountProfileRegistration.Email);


            Establish context = () =>
            {
                _accountProfileRegistration = NewInstanceOf<IProfileRegistration>();
                _accountProfileRegistration.Stub(r => r.Gender).Return(Gender.Female);
                _accountProfileRegistration.Stub(r => r.DisplayName).Return("Display name");
                _accountProfileRegistration.Stub(r => r.Email).Return("Email");
                _accountProfileRegistration.Stub(r => r.ProfileImage).Return(new MemoryStream());

                Injected<IProfileImageCreator>().Stub(c => c.Create(_accountProfileRegistration.ProfileImage)).Return(Task.FromResult(ProfileImage));
            };

            static Profile _result;
            static IProfileRegistration _accountProfileRegistration;
            const string ProfileImage = "profileImage";
        }
    }
}

