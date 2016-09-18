using System.Linq;
using zavit.Domain.Venues.NewVenueCreation;
using zavit.Web.Api.Dtos.Venues;

namespace zavit.Web.Api.DtoServices.Venues.NewVenues
{
    public class NewVenueProvider : INewVenueProvider
    {
        public NewVenue ProvideNewVenueRequest(VenueDetailsDto venueDetailsDto)
        {
            return new NewVenue
            {
                Name = venueDetailsDto.Name,
                ActivityIds = venueDetailsDto.Activities.Select(a => a.Id)
            };
        }
    }
}