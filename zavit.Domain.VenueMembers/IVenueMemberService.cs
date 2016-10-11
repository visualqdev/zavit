using zavit.Domain.Shared.ResultCollections;

namespace zavit.Domain.VenueMembers
{
    public interface IVenueMemberService
    {
        IResultCollection<VenueMember> GetVenueMembers(int venueId, int skip, int take);
    }
}