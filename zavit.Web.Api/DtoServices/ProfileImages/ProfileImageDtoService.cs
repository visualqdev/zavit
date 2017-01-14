using System.IO;
using zavit.Domain.Profiles;
using zavit.Web.Api.DtoFactories.ProfileImages;
using zavit.Web.Api.Dtos.ProfileImages;
using zavit.Web.Core.Context;

namespace zavit.Web.Api.DtoServices.ProfileImages
{
    public class ProfileImageDtoService : IProfileImageDtoService
    {
        readonly IUserContext _userContext;
        readonly IProfileService _profileService;
        readonly IProfileImageUploadDtoFactory _profileImageUploadDtoFactory;

        public ProfileImageDtoService(IUserContext userContext, IProfileService profileService, IProfileImageUploadDtoFactory profileImageUploadDtoFactory)
        {
            _userContext = userContext;
            _profileService = profileService;
            _profileImageUploadDtoFactory = profileImageUploadDtoFactory;
        }

        public ProfileImageUploadDto ChangeProfileImage(Stream imageFile)
        {
            var profile = _userContext.Account.Profile;
            var profileImage = _profileService.UpdateProfileImage(imageFile, profile);
            var profileImageUploadDto = _profileImageUploadDtoFactory.CreateItem(profileImage);

            return profileImageUploadDto;
        }
    }
}