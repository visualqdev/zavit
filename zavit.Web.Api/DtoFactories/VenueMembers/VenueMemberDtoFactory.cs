using System.Linq;
using zavit.Domain.VenueMemberships;
using zavit.Web.Api.DtoFactories.Venues;
using zavit.Web.Api.Dtos.VenueMembers;
using zavit.Web.Api.DtoServices.Profiles;

namespace zavit.Web.Api.DtoFactories.VenueMembers
{
    public class VenueMemberDtoFactory : IVenueMemberDtoFactory
    {
        readonly IVenueActivityDtoFactory _venueActivityDtoFactory;
        readonly IProfileImageUrlBuilder _profileImageUrlBuilder;

        public VenueMemberDtoFactory(IVenueActivityDtoFactory venueActivityDtoFactory, IProfileImageUrlBuilder profileImageUrlBuilder)
        {
            _venueActivityDtoFactory = venueActivityDtoFactory;
            _profileImageUrlBuilder = profileImageUrlBuilder;
        }

        public VenueMemberDto CreateItem(VenueMembership venueMembership)
        {
            var venueActivityDtos = venueMembership.Activities.Select(a => _venueActivityDtoFactory.CreateItem(a));
            var profileImageUrl = _profileImageUrlBuilder.Build(venueMembership.Account.Profile);

            return new VenueMemberDto
            {
                AccountId = venueMembership.Account.Id,
                DisplayName = venueMembership.Account.Profile.DisplayName,
                Activities = venueActivityDtos,
                ProfileImageUrl = profileImageUrl
            };
        }
    }
}