using zavit.Domain.Venues.Search;

namespace zavit.Web.Api.Dtos.Venues
{
    public class VenueSearchCriteriaDto : IVenueSearchCriteria
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int Radius { get; set; }
        public string Name { get; set; }
    }
}