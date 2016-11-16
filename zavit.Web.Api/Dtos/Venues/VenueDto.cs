using System.Collections.Generic;
using zavit.Domain.Venues.NewVenueCreation;

namespace zavit.Web.Api.Dtos.Venues
{
    public class VenueDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string PublicPlaceId { get; set; }
        public string Address { get; set; }
        public IEnumerable<VenueActivityDto> Activities { get; set; }
    }
}