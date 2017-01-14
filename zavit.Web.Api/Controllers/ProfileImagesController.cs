using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using zavit.Domain.Accounts;
using zavit.Domain.Profiles;
using zavit.Web.Api.Authorization.AccessAuthorization;
using zavit.Web.Api.Dtos.ProfileImages;
using zavit.Web.Api.DtoServices.ProfileImages;
using zavit.Web.Core.Context;

namespace zavit.Web.Api.Controllers
{
    public class ProfileImagesController : ApiController
    {
        readonly IProfileImageRepository _profileImageRepository;
        readonly IAccountRepository _accountRepository;
        readonly IUserContext _userContext;
        readonly IProfileService _profileService;
        readonly IProfileImageDtoService _profileImageDtoService;

        public ProfileImagesController(IProfileImageRepository profileImageRepository, IAccountRepository accountRepository, IUserContext userContext, IProfileService profileService, IProfileImageDtoService profileImageDtoService)
        {
            _profileImageRepository = profileImageRepository;
            _accountRepository = accountRepository;
            _userContext = userContext;
            _profileService = profileService;
            _profileImageDtoService = profileImageDtoService;
        }

        [HttpGet]
        [Route("~/api/accounts/{accountId}/profileimage")]
        public HttpResponseMessage GetByAccountId(int accountId)
        {
            var profileImage = _accountRepository.GetProfileImage(accountId);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(profileImage)
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
            return response;
        }

        [HttpGet]
        [Route("~/api/profileimages/{profileImageId}")]
        public HttpResponseMessage Get(int profileImageId)
        {
            var profileImage = _profileImageRepository.Get(profileImageId);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(profileImage.ImageFile)
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
            return response;
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
            var profileImageUploadDto = _profileImageDtoService.ChangeProfileImage(image);

            return profileImageUploadDto;
        }
    }
}