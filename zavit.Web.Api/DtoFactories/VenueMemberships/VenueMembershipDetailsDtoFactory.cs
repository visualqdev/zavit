using System.Linq;
using zavit.Domain.VenueMemberships;
using zavit.Web.Api.DtoFactories.Venues;
using zavit.Web.Api.Dtos.VenueMemberships;

namespace zavit.Web.Api.DtoFactories.VenueMemberships
{
    public class VenueMembershipDetailsDtoFactory : IVenueMembershipDetailsDtoFactory
    {
        readonly IVenueDetailsDtoFactory _venueDetailsDtoFactory;
        readonly IVenueActivityDtoFactory _venueActivityDtoFactory;

        public VenueMembershipDetailsDtoFactory(IVenueDetailsDtoFactory venueDetailsDtoFactory, IVenueActivityDtoFactory venueActivityDtoFactory)
        {
            _venueDetailsDtoFactory = venueDetailsDtoFactory;
            _venueActivityDtoFactory = venueActivityDtoFactory;
        }

        public VenueMembershipDetailsDto CreateItem(VenueMembership venueMembership)
        {
            return new VenueMembershipDetailsDto
            {
                Venue = _venueDetailsDtoFactory.Create(venueMembership.Venue),
                Activities = venueMembership.Activities.Select(a => _venueActivityDtoFactory.CreateItem(a))
            };
        }
    }
}