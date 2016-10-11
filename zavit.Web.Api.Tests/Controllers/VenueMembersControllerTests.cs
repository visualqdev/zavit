using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Web.Api.Controllers;
using zavit.Web.Api.Dtos.VenueMembers;
using zavit.Web.Api.DtoServices.VenueMembers;

namespace zavit.Web.Api.Tests.Controllers 
{
    [Subject("VenueMembersController")]
    public class VenueMembersControllerTests : TestOf<VenueMembersController>
    {
        class When_getting_venue_members
        {
            Because of = () => _result = Subject.Get(VenueId, Skip, Take);

            It should_return_venue_member_dtos_provided_by_the_dto_service = () => _result.ShouldEqual(_venueMembersCollectionDto);

            Establish context = () =>
            {
                _venueMembersCollectionDto = NewInstanceOf<VenueMembersCollectionDto>();
                Injected<IVenueMemberDtoService>().Stub(s => s.GetMembersCollection(VenueId, Skip, Take)).Return(_venueMembersCollectionDto);
            };

            static VenueMembersCollectionDto _result;
            static VenueMembersCollectionDto _venueMembersCollectionDto;
            const int Skip = 1;
            const int Take = 2;
            const int VenueId = 123;
        }
    }
}

