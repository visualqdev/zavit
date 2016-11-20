using System.Collections.Generic;
using System.Threading.Tasks;
using zavit.Web.Api.Dtos.Venues;

namespace zavit.Web.Api.DtoServices.Venues
{
    public interface IVenueDtoService
    {
        Task<VenueDetailsDto> AddVenue(VenueDetailsDto venueDto);
        Task<VenueDetailsDto> GetDefaultVenue(string placeId);
        VenueDetailsDto GetVenue(int venueId);
        Task<IEnumerable<VenueDto>> SuggestVenues(VenueSearchCriteriaDto venueSearchCriteriaDto);
    }
}