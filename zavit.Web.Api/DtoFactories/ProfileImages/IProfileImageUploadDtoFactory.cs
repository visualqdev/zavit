using zavit.Domain.Profiles;
using zavit.Web.Api.Dtos.ProfileImages;

namespace zavit.Web.Api.DtoFactories.ProfileImages
{
    public interface IProfileImageUploadDtoFactory
    {
        ProfileImageUploadDto CreateItem(Profile profile);
    }
}