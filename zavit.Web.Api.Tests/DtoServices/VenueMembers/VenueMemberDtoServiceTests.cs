using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Shared.ResultCollections;
using zavit.Domain.VenueMemberships;
using zavit.Web.Api.DtoFactories.VenueMembers;
using zavit.Web.Api.Dtos.VenueMembers;
using zavit.Web.Api.DtoServices.VenueMembers;
using zavit.Web.Core.Context;

namespace zavit.Web.Api.Tests.DtoServices.VenueMembers 
{
    [Subject("VenueMemberDtoService")]
    public class VenueMemberDtoServiceTests : TestOf<VenueMemberDtoService>
    {
        class When_get_venue_members
        {
            Because of = () => _result = Subject.GetMembersCollection(VenueId, Skip, Take);

            It should_return_venue_member_collection_dto_for_the_venue_id = () => _result.ShouldEqual(_venueMemberCollectionDto);

            Establish context = () =>
            {
                Injected<IUserContext>().Stub(c => c.Account).Return(NewInstanceOf<Account>());

                var venueMemberCollection = NewInstanceOf<IResultCollection<VenueMembership>>();
                Injected<IVenueMembershipService>()
                    .Stub(s => s.GetAllVenueMemberships(VenueId, Skip, Take, Injected<IUserContext>().Account))
                    .Return(venueMemberCollection);

                _venueMemberCollectionDto = NewInstanceOf<VenueMembersCollectionDto>();
                Injected<IVenueMemberCollectionDtoFactory>()
                    .Stub(f => f.CreateItem(venueMemberCollection))
                    .Return(_venueMemberCollectionDto);
            };

            static VenueMembersCollectionDto _result;
            static VenueMembersCollectionDto _venueMemberCollectionDto;
            const int Skip = 1;
            const int Take = 2;
            const int VenueId = 123;
        }
    }
}

