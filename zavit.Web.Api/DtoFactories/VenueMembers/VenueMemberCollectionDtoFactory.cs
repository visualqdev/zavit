using System.Linq;
using zavit.Domain.Shared.ResultCollections;
using zavit.Domain.VenueMemberships;
using zavit.Web.Api.Dtos.VenueMembers;

namespace zavit.Web.Api.DtoFactories.VenueMembers
{
    public class VenueMemberCollectionDtoFactory : IVenueMemberCollectionDtoFactory
    {
        readonly IVenueMemberDtoFactory _venueMemberDtoFactory;

        public VenueMemberCollectionDtoFactory(IVenueMemberDtoFactory venueMemberDtoFactory)
        {
            _venueMemberDtoFactory = venueMemberDtoFactory;
        }

        public VenueMembersCollectionDto CreateItem(IResultCollection<VenueMembership> venueMemberCollection)
        {
            var venueMembers = venueMemberCollection.Results.Select(m => _venueMemberDtoFactory.CreateItem(m));

            return new VenueMembersCollectionDto
            {
                HasMoreResults = venueMemberCollection.HasMoreResults,
                Take = venueMemberCollection.Take,
                Members = venueMembers
            };
        }
    }
}