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
        public HttpResponseMessage Get(int accountId)
        {
            var profileImage = _profileImageRepository.Get(accountId);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(profileImage.ImageFile)
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
            return response;
        }
    }
}