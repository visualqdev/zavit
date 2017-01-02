using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using zavit.Domain.Profiles;

namespace zavit.Web.Api.Controllers
{
    public class ProfileImagesController : ApiController
    {
        readonly IProfileImageRepository _profileImageRepository;

        public ProfileImagesController(IProfileImageRepository profileImageRepository)
        {
            _profileImageRepository = profileImageRepository;
        }

        [HttpGet]
        [Route("~/api/accounts/{accountId}/profileimage")]
        public HttpResponseMessage GetByAccountId(int accountId)
        {
            var profileImage = _profileImageRepository.GetByAccountId(accountId);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(profileImage.ImageFile)
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
    }
}