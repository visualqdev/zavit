using System.Collections.Generic;

namespace zavit.Domain.Venues.NewVenueCreation
{
    public class NewVenue
    {
        public string Name { get; set; }

        public IEnumerable<int> ActivityIds { get; set; }
        public string PublicPlaceId { get; set; }
    }
}