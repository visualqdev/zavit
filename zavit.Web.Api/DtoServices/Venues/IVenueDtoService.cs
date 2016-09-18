using System.Threading.Tasks;
using zavit.Web.Api.Dtos.Venues;

namespace zavit.Web.Api.DtoServices.Venues
{
    public interface IVenueDtoService
    {
        Task<VenueDetailsDto> AddVenue(VenueDetailsDto venueDto, string placeId);
        Task<VenueDetailsDto> GetDefaultVenue(string placeId);
    }
}