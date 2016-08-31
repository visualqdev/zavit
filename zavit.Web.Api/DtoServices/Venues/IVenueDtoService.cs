using System.Threading.Tasks;
using zavit.Web.Api.Dtos.Venues;

namespace zavit.Web.Api.DtoServices.Venues
{
    public interface IVenueDtoService
    {
        Task<VenueDto> AddVenue(VenueDto venueDto, string placeId);
    }
}