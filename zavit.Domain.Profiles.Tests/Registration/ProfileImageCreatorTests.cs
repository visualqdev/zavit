using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Profiles.Registration;

namespace zavit.Domain.Profiles.Tests.Registration 
{
    [Subject("ProfileImageCreator")]
    public class ProfileImageCreatorTests : TestOf<ProfileImageCreator>
    {
        class When_creating_profile_image
        {
            Because of = () => _result = Subject.Create(_accountProfileRegistration);

            It should_set_the_image_file_to_be_same_as_accout_profile_registration = () => _result.ImageFile.ShouldEqual(_imageFile);

            Establish context = () =>
            {
                _imageFile = new byte[] {1, 0, 1};

                _accountProfileRegistration = NewInstanceOf<IProfileRegistration>();
                _accountProfileRegistration.Stub(r => r.ProfileImage).Return(_imageFile);
            };

            static IProfileRegistration _accountProfileRegistration;
            static ProfileImage _result;
            static byte[] _imageFile;
        }

        class When_creating_profile_image_but_there_is_no_image_file_provided_with_registration
        {
            Because of = () => _result = Subject.Create(_accountProfileRegistration);

            It should_always_return_null = () => _result.ShouldBeNull();

            Establish context = () =>
            {
                _accountProfileRegistration = NewInstanceOf<IProfileRegistration>();
            };

            static IProfileRegistration _accountProfileRegistration;
            static ProfileImage _result;
        }
    }
}

