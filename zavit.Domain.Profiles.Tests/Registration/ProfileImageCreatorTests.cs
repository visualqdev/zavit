using System.IO;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Profiles.Registration;
using zavit.Domain.Shared.Images;

namespace zavit.Domain.Profiles.Tests.Registration 
{
    [Subject("ProfileImageCreator")]
    public class ProfileImageCreatorTests : TestOf<ProfileImageCreator>
    {
        class When_creating_profile_image
        {
            Because of = () => _result = Subject.Create(_imageFile);

            It should_set_the_image_file_to_be_resized_image = () => _result.ImageFile.ShouldEqual(_image);

            Establish context = () =>
            {
                _imageFile = new MemoryStream();

                _image = new byte[] { 1, 0, 1 };
                Injected<IImageResizer>()
                    .Stub(r => r.ResizeImageToMinimum(_imageFile, ProfileImageCreator.ProfileImageDimension, ProfileImageCreator.ProfileImageDimension))
                    .Return(_image);
            };

            static ProfileImage _result;
            static Stream _imageFile;
            static byte[] _image;
        }

        class When_creating_profile_image_but_there_is_no_image_file_provided_with_registration
        {
            Because of = () => _result = Subject.Create(null);

            It should_always_return_null = () => _result.ShouldBeNull();

            static ProfileImage _result;
        }
    }
}

