using System.IO;
using System.Threading.Tasks;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Infrastructure.Profiles.ProfileImages;
using zavit.Infrastructure.Storage;

namespace zavit.Infrastructure.Profiles.Tests.ProfileImages 
{
    [Subject("ProfileImageStorage")]
    public class ProfileImageStorageTests : TestOf<ProfileImageStorage>
    {
        class When_saving_profile_image
        {
            Because of = () => Subject.SaveImage(ImageName, _imageFile).Wait();

            It should_store_the_image_in_an_image_container_under_profile_images_path = 
                () => Injected<IFileStorage>().AssertWasCalled(s => s.UploadAsync(ProfileImageStorage.ContainerName, ImageName + ".jpg", _imageFile));

            Establish context = () =>
            {
                _imageFile = new MemoryStream();

                Injected<IFileStorage>()
                    .Stub(s => s.UploadAsync(ProfileImageStorage.ContainerName, ImageName + ".jpg", _imageFile))
                    .Return(Task.FromResult(0));
            };

            static Stream _imageFile;
            const string ImageName = "TestImage";
        }

        class When_getting_profile_image_path
        {
            Because of = () => _result = Subject.ImageUrl(ImageName);

            It should_return_full_image_url_to_the_storage = () => _result.ShouldEqual("https://storage.com/profileimages/ImageName.jpg");

            Establish context = () =>
            {
                Injected<IStorageConfig>().Stub(c => c.StorageUrl).Return("https://storage.com/");
            };

            static string _result;
            const string ImageName = "ImageName";
        }

        class When_getting_profile_image_path_and_the_image_name_is_not_provided
        {
            Because of = () => _result = Subject.ImageUrl(null);

            It should_return_null = () => _result.ShouldBeNull();

            static string _result;
        }
    }
}

