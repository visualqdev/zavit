using System;
using System.Web.Http;
using zavit.Web.Api.Authorization.AccessAuthorization;
using zavit.Web.Api.Dtos.Profiles;
using zavit.Web.Api.DtoServices.Profiles;

namespace zavit.Web.Api.Controllers
{
    public class ProfilesController : ApiController
    {
        readonly IProfileDtoService _profileDtoService;
        public const string GetMyProfileRoute = "getMyProfileRoute";

        public ProfilesController(IProfileDtoService profileDtoService)
        {
            _profileDtoService = profileDtoService;
        }

        [HttpPost]
        [AccessAuthorize]
        [Route("~/api/profiles")]
        public IHttpActionResult Post(ProfileDto profileDto)
        {
            var updatedProfile = _profileDtoService.Update(profileDto);
            return CreatedAtRoute(GetMyProfileRoute, new {}, updatedProfile);
        }

        [HttpGet]
        [AccessAuthorize]
        [Route("~/api/profiles/myprofile", Name = GetMyProfileRoute)]
        public ProfileDto GetMyProfile()
        {
            return _profileDtoService.GetMyProfile();
        }
    }
}