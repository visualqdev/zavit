using System.IO;
using System.Threading.Tasks;
using zavit.Web.Api.Dtos.ProfileImages;

namespace zavit.Web.Api.DtoServices.ProfileImages
{
    public interface IProfileImageDtoService
    {
        Task<ProfileImageUploadDto> ChangeProfileImage(Stream imageFile);
    }
}