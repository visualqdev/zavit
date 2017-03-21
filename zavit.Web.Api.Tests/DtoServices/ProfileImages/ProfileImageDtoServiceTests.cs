using System.IO;
using System.Threading.Tasks;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Profiles;
using zavit.Web.Api.DtoFactories.ProfileImages;
using zavit.Web.Api.Dtos.ProfileImages;
using zavit.Web.Api.DtoServices.ProfileImages;
using zavit.Web.Core.Context;

namespace zavit.Web.Api.Tests.DtoServices.ProfileImages 
{
    [Subject("ProfileImageDtoService")]
    public class ProfileImageDtoServiceTests : TestOf<ProfileImageDtoService>
    {
        class When_changing_profile_image
        {
            Because of = () => _result = Subject.ChangeProfileImage(_imageFile).Result;

            It should_return_profile_image_upload_dto_for_the_updated_profile = () => _result.ShouldEqual(_profileImageUploadDto);

            Establish context = () =>
            {
                var account = NewInstanceOf<Account>();
                account.Profile = NewInstanceOf<Profile>();
                Injected<IUserContext>().Stub(c => c.Account).Return(account);

                _imageFile = new MemoryStream();
                
                var profile = NewInstanceOf<Profile>();
                Injected<IProfileService>()
                    .Stub(s => s.UpdateProfileImage(_imageFile, account.Profile))
                    .Return(Task.FromResult(profile));
                
                _profileImageUploadDto = NewInstanceOf<ProfileImageUploadDto>();
                Injected<IProfileImageUploadDtoFactory>().Stub(f => f.CreateItem(profile)).Return(_profileImageUploadDto);
            };

            static Stream _imageFile;
            static ProfileImageUploadDto _result;
            static ProfileImageUploadDto _profileImageUploadDto;
        }
    }
}

