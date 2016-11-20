using System.Linq;
using zavit.Domain.VenueMemberships;
using zavit.Web.Api.DtoFactories.Venues;
using zavit.Web.Api.Dtos.VenueMemberships;

namespace zavit.Web.Api.DtoFactories.VenueMemberships
{
    public class VenueMembershipDtoFactory : IVenueMembershipDtoFactory
    {
        readonly IVenueActivityDtoFactory _venueActivityDtoFactory;
        readonly IMembershipVenueDtoFactory _membershipVenueDtoFactory;

        public VenueMembershipDtoFactory(IVenueActivityDtoFactory venueActivityDtoFactory, IMembershipVenueDtoFactory membershipVenueDtoFactory)
        {
            _venueActivityDtoFactory = venueActivityDtoFactory;
            _membershipVenueDtoFactory = membershipVenueDtoFactory;
        }

        public VenueMembershipDto CreateItem(VenueMembership venueMembership)
        {
            var activityDtos = venueMembership.Activities.Select(a => _venueActivityDtoFactory.CreateItem(a));
            var membershipVenueDto = _membershipVenueDtoFactory.Create(venueMembership.Venue);

            return new VenueMembershipDto
            {
                Venue = membershipVenueDto,
                Activities = activityDtos
            };
        }
    }
}