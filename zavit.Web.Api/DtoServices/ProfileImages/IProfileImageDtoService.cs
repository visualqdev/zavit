using System.IO;
using zavit.Web.Api.Dtos.ProfileImages;

namespace zavit.Web.Api.DtoServices.ProfileImages
{
    public interface IProfileImageDtoService
    {
        ProfileImageUploadDto ChangeProfileImage(Stream imageFile);
    }
}