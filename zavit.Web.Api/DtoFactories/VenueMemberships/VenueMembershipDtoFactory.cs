using System.Linq;
using zavit.Domain.VenueMemberships;
using zavit.Web.Api.DtoFactories.Venues;
using zavit.Web.Api.Dtos.VenueMemberships;

namespace zavit.Web.Api.DtoFactories.VenueMemberships
{
    public class VenueMembershipDtoFactory : IVenueMembershipDtoFactory
    {
        readonly IVenueActivityDtoFactory _venueActivityDtoFactory;
        readonly IVenueDetailsDtoFactory _venueDetailsDtoFactory;

        public VenueMembershipDtoFactory(IVenueActivityDtoFactory venueActivityDtoFactory, IVenueDetailsDtoFactory venueDetailsDtoFactory)
        {
            _venueActivityDtoFactory = venueActivityDtoFactory;
            _venueDetailsDtoFactory = venueDetailsDtoFactory;
        }

        public VenueMembershipDto CreateItem(VenueMembership venueMembership)
        {
            var activityDtos = venueMembership.Activities.Select(a => _venueActivityDtoFactory.CreateItem(a));
            var venueDetailsDto = _venueDetailsDtoFactory.Create(venueMembership.Venue);

            return new VenueMembershipDto
            {
                Venue = venueDetailsDto,
                Activities = activityDtos
            };
        }
    }
}