using zavit.Domain.Venues;
using zavit.Web.Api.Dtos.Venues;

namespace zavit.Web.Api.DtoFactories.Venues
{
    public class VenueDtoFactory : IVenueDtoFactory
    {
        public VenueDto Create(IVenue venue)
        {
            return new VenueDto
            {
                Id = venue.Id,
                Name = venue.Name
            };
        }
    }
}