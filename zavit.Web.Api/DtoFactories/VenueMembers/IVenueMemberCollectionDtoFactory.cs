using zavit.Domain.Shared.ResultCollections;
using zavit.Domain.VenueMemberships;
using zavit.Web.Api.Dtos.VenueMembers;

namespace zavit.Web.Api.DtoFactories.VenueMembers
{
    public interface IVenueMemberCollectionDtoFactory
    {
        VenueMembersCollectionDto CreateItem(IResultCollection<VenueMembership> venueMemberCollection);
    }
}