using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Activities;
using zavit.Domain.VenueMemberships.NewVenueMembershipCreation;
using zavit.Domain.Venues;

namespace zavit.Domain.VenueMemberships.Tests.VenueMembershipCreation 
{
    [Subject("VenueMembershipCreator")]
    public class VenueMembershipCreatorTests : TestOf<VenueMembershipCreator>
    {
        class When_creating_a_venue_membership
        {
            Because of = () => _result = Subject.Create(_account, _newVenueMembership);

            It should_set_the_user_account_to_be_the_provided_account = () => _result.Account.ShouldEqual(_account);

            It should_set_the_venue_to_be_the_venue_that_matches_the_requested_new_venue_membership =
                () => _result.Venue.ShouldEqual(_venue);

            It should_set_the_activities_to_match_the_requested_new_venue_membership_activities =
                () => _result.Activities.ShouldEqual(_activities);

            Establish context = () =>
            {
                _account = NewInstanceOf<Account>();

                _newVenueMembership = NewInstanceOf<NewVenueMembership>();
                _newVenueMembership.VenueId = 123;
                _newVenueMembership.Activities = new[] { 45, 67 };

                _venue = NewInstanceOf<Venue>();
                Injected<IVenueRepository>().Stub(r => r.GetVenue(_newVenueMembership.VenueId)).Return(_venue);

                _activities = new[] { NewInstanceOf<Activity>(), NewInstanceOf<Activity>() };
                Injected<IActivityRepository>().Stub(r => r.GetActivities(_newVenueMembership.Activities)).Return(_activities);
            };

            static VenueMembership _result;
            static Account _account;
            static NewVenueMembership _newVenueMembership;
            static Venue _venue;
            static IEnumerable<Activity> _activities;
        }
    }
}

