using zavit.Web.Api.Dtos.Venues;

namespace zavit.Web.Api.DtoServices.Venues
{
    public class VenueDtoService : IVenueDtoService
    {
        public VenueDto AddVenue(VenueDto venueDto, string placeId)
        {
            return new VenueDto { Id = 123 };
        }
    }
}