using System.Linq;
using zavit.Domain.VenueMemberships;
using zavit.Web.Api.DtoFactories.Venues;
using zavit.Web.Api.Dtos.VenueMembers;

namespace zavit.Web.Api.DtoFactories.VenueMembers
{
    public class VenueMemberDtoFactory : IVenueMemberDtoFactory
    {
        readonly IVenueActivityDtoFactory _venueActivityDtoFactory;

        public VenueMemberDtoFactory(IVenueActivityDtoFactory venueActivityDtoFactory)
        {
            _venueActivityDtoFactory = venueActivityDtoFactory;
        }

        public VenueMemberDto CreateItem(VenueMembership venueMembership)
        {
            var venueActivityDtos = venueMembership.Activities.Select(a => _venueActivityDtoFactory.CreateItem(a));

            return new VenueMemberDto
            {
                AccountId = venueMembership.Account.Id,
                DisplayName = venueMembership.Account.Profile.DisplayName,
                Activities = venueActivityDtos
            };
        }
    }
}