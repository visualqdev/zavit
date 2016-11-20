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
    [Subject("VenueMembershipDetailsDtoFactory")]
    public class VenueMembershipDetailsDtoFactoryTests : TestOf<VenueMembershipDetailsDtoFactory>
    {
        class When_creating_the_venue_membership_details_dto_from_venue_membership
        {
            Because of = () => _result = Subject.CreateItem(_venueMembership);

            It should_set_the_membership_venue_to_venue_details_dto = () => _result.Venue.ShouldEqual(_venueDetailsDto);

            It should_create_a_venue_activity_dto_for_each_membership_activity =
                () => _result.Activities.ShouldContainOnly(_membershipActivity, _otherMembershipActivity);

            Establish context = () =>
            {
                var activity = NewInstanceOf<Activity>();
                var otherActivity = NewInstanceOf<Activity>();

                _venueMembership = NewInstanceOf<VenueMembership>();
                _venueMembership.Venue = NewInstanceOf<Venue>();
                _venueMembership.Activities = new[] { activity, otherActivity };

                _venueDetailsDto = NewInstanceOf<VenueDetailsDto>();
                Injected<IVenueDetailsDtoFactory>().Stub(f => f.Create(_venueMembership.Venue)).Return(_venueDetailsDto);

                _membershipActivity = NewInstanceOf<VenueActivityDto>();
                Injected<IVenueActivityDtoFactory>().Stub(f => f.CreateItem(activity)).Return(_membershipActivity);

                _otherMembershipActivity = NewInstanceOf<VenueActivityDto>();
                Injected<IVenueActivityDtoFactory>().Stub(f => f.CreateItem(otherActivity)).Return(_otherMembershipActivity);
            };

            static VenueMembership _venueMembership;
            static VenueMembershipDetailsDto _result;
            static VenueDetailsDto _venueDetailsDto;
            static VenueActivityDto _otherMembershipActivity;
            static VenueActivityDto _membershipActivity;
        }

        class When_creating_the_venue_membership_details_dto_from_venue
        {
            Because of = () => _result = Subject.CreateItem(_venue);

            It should_set_the_membership_venue_to_venue_details_dto = () => _result.Venue.ShouldEqual(_venueDetailsDto);

            It should_set_the_activities_to_be_an_empty_collection = () => _result.Activities.ShouldBeEmpty();

            Establish context = () =>
            {
                _venue = NewInstanceOf<Venue>();

                _venueDetailsDto = NewInstanceOf<VenueDetailsDto>();
                Injected<IVenueDetailsDtoFactory>().Stub(f => f.Create(_venue)).Return(_venueDetailsDto);
            };

            static Venue _venue;
            static VenueMembershipDetailsDto _result;
            static VenueDetailsDto _venueDetailsDto;
        }
    }
}

