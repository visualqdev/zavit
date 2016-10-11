using System.Collections.Generic;
using zavit.Web.Api.Dtos.VenueMembers;

namespace zavit.Web.Api.DtoServices.VenueMembers
{
    public interface IVenueMemberDtoService
    {
        VenueMembersCollectionDto GetMembersCollection(int venueId, int skip, int take);
    }
}