using zavit.Web.Api.Dtos.Venues;

namespace zavit.Web.Api.DtoServices.Venues
{
    public interface IVenueDtoService
    {
        VenueDto AddVenue(VenueDto venueDto, string placeId);
    }
}