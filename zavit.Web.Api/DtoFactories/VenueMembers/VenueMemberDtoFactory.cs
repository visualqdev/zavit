using System.Linq;
using zavit.Domain.Profiles.ProfileImages;
using zavit.Domain.VenueMemberships;
using zavit.Web.Api.DtoFactories.Venues;
using zavit.Web.Api.Dtos.VenueMembers;

namespace zavit.Web.Api.DtoFactories.VenueMembers
{
    public class VenueMemberDtoFactory : IVenueMemberDtoFactory
    {
        readonly IVenueActivityDtoFactory _venueActivityDtoFactory;
        readonly IProfileImageStorage _profileImageStorage;

        public VenueMemberDtoFactory(IVenueActivityDtoFactory venueActivityDtoFactory, IProfileImageStorage profileImageStorage)
        {
            _venueActivityDtoFactory = venueActivityDtoFactory;
            _profileImageStorage = profileImageStorage;
        }

        public VenueMemberDto CreateItem(VenueMembership venueMembership)
        {
            var venueActivityDtos = venueMembership.Activities.Select(a => _venueActivityDtoFactory.CreateItem(a));
            var profileImageUrl = _profileImageStorage.ImageUrl(venueMembership.Account.Profile.ProfileImage);

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