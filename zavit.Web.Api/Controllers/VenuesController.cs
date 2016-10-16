using System;
using System.Threading.Tasks;
using System.Web.Http;
using zavit.Web.Api.Authorization.AccessAuthorization;
using zavit.Web.Api.Dtos.Venues;
using zavit.Web.Api.DtoServices.Venues;

namespace zavit.Web.Api.Controllers
{
    public class VenuesController : ApiController
    {
        const string PostRoute = "VenuesPost";
        const string GetDefaultRoute = "VenuesDefaultGet";
        const string GetSingleRoute = "VenuesGet";

        readonly IVenueDtoService _venueDtoService;

        public VenuesController(IVenueDtoService venueDtoService)
        {
            _venueDtoService = venueDtoService;
        }
        
        [HttpGet]
        [Route("~/api/places/{placeid}/venues/default", Name = GetDefaultRoute)]
        public async Task<VenueDetailsDto> GetDefault(string placeId)
        {
            return await _venueDtoService.GetDefaultVenue(placeId);
        }

        [HttpGet]
        [Route("~/api/venues/{venueId}", Name = GetSingleRoute)]
        public VenueDetailsDto GetVenue(int venueId)
        {
            return _venueDtoService.GetVenue(venueId);
        }

        [AccessAuthorize]
        [HttpPost]
        [Route("~/api/places/{placeid}/venues", Name = PostRoute)]
        public async Task<IHttpActionResult> Post(VenueDetailsDto venueDto, string placeId)
        {
            var venue = await _venueDtoService.AddVenue(venueDto, placeId);
            return CreatedAtRoute(CommonRoutes.Default, new { controller = "venues", id = venue.Id }, venue);
        }
    }
}