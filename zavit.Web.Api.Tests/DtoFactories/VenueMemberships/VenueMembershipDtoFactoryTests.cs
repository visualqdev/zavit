using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Activities;
using zavit.Domain.VenueMemberships;
using zavit.Domain.Venues;
using zavit.Web.Api.DtoFactories.VenueMemberships;
using zavit.Web.Api.DtoFactories.Venues;
using zavit.Web.Api.Dtos.VenueMemberships;
using zavit.Web.Api.Dtos.Venues;

namespace zavit.Web.Api.Tests.DtoFactories.VenueMemberships 
{
    [Subject("VenueMembershipDtoFactory")]
    public class VenueMembershipDtoFactoryTests : TestOf<VenueMembershipDtoFactory>
    {
        class When_creating_a_venue_dto
        {
            Because of = () => _result = Subject.CreateItem(_venueMembership);

            It should_set_the_venue_to_be_the_venue_details_dto_for_the_membership_venue = 
                () => _result.Venue.ShouldEqual(_venueDetailsDto);

            It should_create_a_venue_activity_dto_for_each_venue_membership_activity =
                () => _result.Activities.ShouldContainOnly(_activityDto, _otherActivityDto);

            Establish context = () =>
            {
                _venueMembership = NewInstanceOf<VenueMembership>();
                _venueMembership.Venue = NewInstanceOf<Venue>();

                _venueDetailsDto = NewInstanceOf<MembershipVenueDto>();
                Injected<IMembershipVenueDtoFactory>().Stub(f => f.Create(_venueMembership.Venue)).Return(_venueDetailsDto);

                var activity = NewInstanceOf<Activity>();
                var otherActivity = NewInstanceOf<Activity>();
                _venueMembership.Activities = new[] { activity, otherActivity };

                _activityDto = NewInstanceOf<VenueActivityDto>();
                Injected<IVenueActivityDtoFactory>().Stub(f => f.CreateItem(activity)).Return(_activityDto);

                _otherActivityDto = NewInstanceOf<VenueActivityDto>();
                Injected<IVenueActivityDtoFactory>().Stub(f => f.CreateItem(otherActivity)).Return(_otherActivityDto);
            };

            static VenueMembership _venueMembership;
            static VenueMembershipDto _result;
            static VenueActivityDto _activityDto;
            static VenueActivityDto _otherActivityDto;
            static MembershipVenueDto _venueDetailsDto;
        }
    }
}

