using Machine.Specifications;
using Rhino.Mspec.Contrib;
using zavit.Domain.VenueMemberships.NewVenueMembershipCreation;
using zavit.Web.Api.Dtos.VenueMemberships;
using zavit.Web.Api.Dtos.Venues;
using zavit.Web.Api.DtoServices.VenueMemberships.NewVenueMemberships;

namespace zavit.Web.Api.Tests.DtoFactories.VenueMemberships.NewVenueMemberships 
{
    [Subject("NewVenueMembershipProvider")]
    public class NewVenueMembershipProviderTests : TestOf<NewVenueMembershipProvider>
    {
        class When_providing_new_user_venue
        {
            Because of = () => _result = Subject.Provide(_venueMembershipDto);

            It should_set_the_venue_id_to_be_the_same_as_venue_membership_dto_venue_id = 
                () => _result.VenueId.ShouldEqual(_venueMembershipDto.VenueId);

            It should_set_the_activities_to_be_the_venue_membership_dto_activity_ids =
                () => _result.Activities.ShouldContainOnly(_activity.Id, _otherActivity.Id);

            Establish context = () =>
            {
                _venueMembershipDto = NewInstanceOf<VenueMembershipDto>();
                _venueMembershipDto.VenueId = 123;

                _activity = NewInstanceOf<VenueActivityDto>();
                _activity.Id = 1;
                _otherActivity = NewInstanceOf<VenueActivityDto>();
                _otherActivity.Id = 2;
                _venueMembershipDto.Activities = new[] {_activity, _otherActivity};
            };

            static VenueMembershipDto _venueMembershipDto;
            static NewVenueMembership _result;
            static VenueActivityDto _activity;
            static VenueActivityDto _otherActivity;
        }
    }
}

