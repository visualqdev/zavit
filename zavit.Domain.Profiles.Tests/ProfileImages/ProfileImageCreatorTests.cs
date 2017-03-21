using System.IO;
using System.Threading.Tasks;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Profiles.ProfileImages;
using zavit.Domain.Profiles.Registration;
using zavit.Domain.Shared;
using zavit.Domain.Shared.Images;

namespace zavit.Domain.Profiles.Tests.ProfileImages 
{
    [Subject("ProfileImageCreator")]
    public class ProfileImageCreatorTests : TestOf<ProfileImageCreator>
    {
        class When_creating_profile_image
        {
            Because of = () => _result = Subject.Create(_imageFile).Result;

            It should_store_the_resized_image_file_in_the_image_storage = () =>
                Injected<IProfileImageStorage>().AssertWasCalled(s => s.SaveImage(ImageGuid, _image));

            It should_return_the_generated_image_name = () => _result.ShouldEqual(ImageGuid);

            Establish context = () =>
            {
                _imageFile = new MemoryStream();

                Injected<IGuid>().Stub(g => g.NewGuidString()).Return(ImageGuid);

                _image = new MemoryStream();
                Injected<IImageResizer>()
                    .Stub(r => r.ResizeImageToMinimum(_imageFile, ProfileImageCreator.ProfileImageDimension, ProfileImageCreator.ProfileImageDimension))
                    .Return(_image);

                Injected<IProfileImageStorage>().Stub(s => s.SaveImage(ImageGuid, _image)).Return(Task.FromResult(0));
            };

            static string _result;
            static Stream _imageFile;
            static Stream _image;
            const string ImageGuid = "randomGuidString";
        }

        class When_creating_profile_image_but_there_is_no_image_file_provided_with_registration
        {
            Because of = () => _result = Subject.Create(null).Result;

            It should_always_return_null = () => _result.ShouldBeNull();

            static string _result;
        }
    }
}

