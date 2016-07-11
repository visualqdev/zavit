using zavit.Domain.Venues.NewVenueCreation;

namespace zavit.Web.Api.Dtos.Venues
{
    public class VenueDto : INewVenue
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}