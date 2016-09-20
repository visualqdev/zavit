using System.Linq;
using zavit.Domain.VenueMemberships;
using zavit.Web.Api.DtoFactories.Venues;
using zavit.Web.Api.Dtos.VenueMemberships;

namespace zavit.Web.Api.DtoFactories.VenueMemberships
{
    public class VenueMembershipDtoFactory : IVenueMembershipDtoFactory
    {
        readonly IVenueActivityDtoFactory _venueActivityDtoFactory;

        public VenueMembershipDtoFactory(IVenueActivityDtoFactory venueActivityDtoFactory)
        {
            _venueActivityDtoFactory = venueActivityDtoFactory;
        }

        public VenueMembershipDto CreateItem(VenueMembership venueMembership)
        {
            var activityDtos = venueMembership.Activities.Select(a => _venueActivityDtoFactory.CreateItem(a));

            return new VenueMembershipDto
            {
                VenueId = venueMembership.Venue.Id,
                Activities = activityDtos
            };
        }
    }
}