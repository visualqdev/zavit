using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Shared.ResultCollections;
using zavit.Domain.VenueMemberships;
using zavit.Web.Api.DtoFactories.VenueMembers;
using zavit.Web.Api.Dtos.VenueMembers;

namespace zavit.Web.Api.Tests.DtoFactories.VenueMembers 
{
    [Subject("VenueMemberCollectionDtoFactory")]
    public class VenueMemberCollectionDtoFactoryTests : TestOf<VenueMemberCollectionDtoFactory>
    {
        class When_creating_a_venue_members_collection
        {
            Because of = () => _result = Subject.CreateItem(_venueMembersCollection);

            It should_return_set_the_has_more_results_property_to_be_the_same_as_result_collection = 
                () => _result.HasMoreResults.ShouldEqual(_venueMembersCollection.HasMoreResults);

            It should_return_set_the_take_property_to_be_the_same_as_result_collection =
                () => _result.Take.ShouldEqual(_venueMembersCollection.Take);

            It should_return_a_venue_member_dto_for_each_venue_membership_in_result_collection =
                () => _result.Members.ShouldContainOnly(_venueMemberDto, _otherVenueMemberDto);

            Establish context = () =>
            {
                var venueMembership = NewInstanceOf<VenueMembership>();
                var otherVenueMembership = NewInstanceOf<VenueMembership>();

                _venueMembersCollection = NewInstanceOf<IResultCollection<VenueMembership>>();
                _venueMembersCollection.Stub(c => c.HasMoreResults).Return(true);
                _venueMembersCollection.Stub(c => c.Take).Return(2);
                _venueMembersCollection.Stub(c => c.Results).Return(new[] {venueMembership, otherVenueMembership});

                _venueMemberDto = NewInstanceOf<VenueMemberDto>();
                _otherVenueMemberDto = NewInstanceOf<VenueMemberDto>();
                Injected<IVenueMemberDtoFactory>().Stub(f => f.CreateItem(venueMembership)).Return(_venueMemberDto);
                Injected<IVenueMemberDtoFactory>().Stub(f => f.CreateItem(otherVenueMembership)).Return(_otherVenueMemberDto);
            };

            static IResultCollection<VenueMembership> _venueMembersCollection;
            static VenueMembersCollectionDto _result;
            static VenueMemberDto _venueMemberDto;
            static VenueMemberDto _otherVenueMemberDto;
        }
    }
}

