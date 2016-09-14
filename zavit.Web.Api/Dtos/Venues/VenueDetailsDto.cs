using System.Collections.Generic;

namespace zavit.Web.Api.Dtos.Venues
{
    public class VenueDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<VenueActivityDto> Activities { get; set; }
    }
}