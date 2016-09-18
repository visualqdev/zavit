using zavit.Domain.Venues.NewVenueCreation;
using zavit.Web.Api.Dtos.Venues;

namespace zavit.Web.Api.DtoServices.Venues.NewVenues
{
    public interface INewVenueProvider
    {
        NewVenue ProvideNewVenueRequest(VenueDetailsDto venueDetailsDto);
    }
}