using zavit.Domain.Venues;
using zavit.Web.Api.Dtos.Venues;

namespace zavit.Web.Api.DtoFactories.Venues
{
    public interface IVenueDetailsDtoFactory
    {
        VenueDetailsDto Create(Venue venue);
    }
}