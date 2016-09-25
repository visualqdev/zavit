using System.Linq;
using zavit.Domain.VenueMemberships.NewVenueMembershipCreation;
using zavit.Web.Api.Dtos.VenueMemberships;

namespace zavit.Web.Api.DtoServices.VenueMemberships.NewVenueMemberships
{
    public class NewVenueMembershipProvider : INewVenueMembershipProvider
    {
        public NewVenueMembership Provide(VenueMembershipDto venueMembershipDto)
        {
            return new NewVenueMembership
            {
                VenueId = venueMembershipDto.Venue.Id,
                Activities = venueMembershipDto.Activities.Select(a => a.Id)
            };
        }
    }
}