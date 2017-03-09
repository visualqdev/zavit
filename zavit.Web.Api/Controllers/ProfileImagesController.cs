using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using zavit.Web.Api.Authorization.AccessAuthorization;
using zavit.Web.Api.Dtos.ProfileImages;
using zavit.Web.Api.DtoServices.ProfileImages;

namespace zavit.Web.Api.Controllers
{
    public class ProfileImagesController : ApiController
    {
        readonly IProfileImageDtoService _profileImageDtoService;

        public ProfileImagesController(IProfileImageDtoService profileImageDtoService)
        {
            _profileImageDtoService = profileImageDtoService;
        }

        [HttpPost]
        [AccessAuthorize]
        [Route("~/api/profileimages")]
        public async Task<ProfileImageUploadDto> Post()
        {
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            var streamProvider = await Request.Content.ReadAsMultipartAsync();
            if (streamProvider.Contents.Count < 1) return null;
            
            var file = streamProvider.Contents[0];
            var image = await file.ReadAsStreamAsync();
            var profileImageUploadDto = await _profileImageDtoService.ChangeProfileImage(image);

            return profileImageUploadDto;
        }
    }
}