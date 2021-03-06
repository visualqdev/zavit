﻿using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Activities;
using zavit.Domain.Shared.ResultCollections;
using zavit.Domain.VenueMemberships.NewVenueMembershipCreation;

namespace zavit.Domain.VenueMemberships.Tests 
{
    [Subject("VenueMembershipService")]
    public class VenueMembershipServiceTests : TestOf<VenueMembershipService>
    {
        class When_adding_a_user_to_venue
        {
            Because of = () => _result = Subject.AddUserToVenue(_account, _newVenueMembership);

            It should_return_a_newly_created_venue_membership = () => _result.ShouldEqual(_venueMembership);

            It should_store_new_venue_in_the_repository =
                () => Injected<IVenueMembershipRepository>().AssertWasCalled(r => r.Save(_venueMembership));

            Establish context = () =>
            {
                _account = NewInstanceOf<Account>();

                _newVenueMembership = NewInstanceOf<NewVenueMembership>();

                _venueMembership = NewInstanceOf<VenueMembership>();
                Injected<IVenueMembershipCreator>().Stub(c => c.Create(_account, _newVenueMembership)).Return(_venueMembership);
            };

            static Account _account;
            static NewVenueMembership _newVenueMembership;
            static VenueMembership _result;
            static VenueMembership _venueMembership;
        }

        class When_trying_to_add_a_user_to_the_venue_but_the_user_is_already_a_member_with_different_activities
        {
            Because of = () => _result = Subject.AddUserToVenue(_account, _newVenueMembership);

            It should_update_the_membership = () => Injected<IVenueMembershipRepository>().AssertWasCalled(r => r.Update(_venueMembership));

            It should_return_the_exisitng_venue_membership = () => _result.ShouldEqual(_venueMembership);

            Establish context = () =>
            {
                _account = NewInstanceOf<Account>();
                _account.Id = 123;

                _newVenueMembership = NewInstanceOf<NewVenueMembership>();
                _newVenueMembership.VenueId = 456;
                _newVenueMembership.Activities = new List<int> { 1, 2 };

                _activities = new List<Activity> { NewInstanceOf<Activity>(), NewInstanceOf<Activity>() };
                Injected<IActivityRepository>()
                    .Stub(r => r.GetActivities(_newVenueMembership.Activities))
                    .Return(_activities);

                _venueMembership = NewInstanceOf<VenueMembership>();
                _venueMembership.Stub(m => m.UpdateActivities(_activities)).Return(true);

                Injected<IVenueMembershipRepository>()
                    .Stub(r => r.GetMembership(_account.Id, _newVenueMembership.VenueId))
                    .Return(_venueMembership);
            };

            static Account _account;
            static NewVenueMembership _newVenueMembership;
            static VenueMembership _result;
            static VenueMembership _venueMembership;
            static List<Activity> _activities;
        }

        class When_trying_to_add_a_user_to_the_venue_but_the_user_is_already_a_member_with_same_activities
        {
            Because of = () => _result = Subject.AddUserToVenue(_account, _newVenueMembership);

            It should_not_try_update_the_membership = () => Injected<IVenueMembershipRepository>().AssertWasNotCalled(r => r.Update(_venueMembership));

            It should_return_the_exisitng_venue_membership = () => _result.ShouldEqual(_venueMembership);

            Establish context = () =>
            {
                _account = NewInstanceOf<Account>();
                _account.Id = 123;

                _newVenueMembership = NewInstanceOf<NewVenueMembership>();
                _newVenueMembership.VenueId = 456;
                _newVenueMembership.Activities = new List<int> { 1, 2 };

                _activities = new List<Activity> { NewInstanceOf<Activity>(), NewInstanceOf<Activity>() };
                Injected<IActivityRepository>()
                    .Stub(r => r.GetActivities(_newVenueMembership.Activities))
                    .Return(_activities);

                _venueMembership = NewInstanceOf<VenueMembership>();
                _venueMembership.Stub(m => m.UpdateActivities(_activities)).Return(false);

                Injected<IVenueMembershipRepository>()
                    .Stub(r => r.GetMembership(_account.Id, _newVenueMembership.VenueId))
                    .Return(_venueMembership);
            };

            static Account _account;
            static NewVenueMembership _newVenueMembership;
            static VenueMembership _result;
            static VenueMembership _venueMembership;
            static List<Activity> _activities;
        }

        class When_getting_venue_memberships
        {
            Because of = () => _result = Subject.GetVenueMembershipsForUser(_account);

            It should_return_the_venue_memberships_from_the_repository = () => _result.ShouldEqual(_venueMemberships);

            Establish context = () =>
            {
                _account = NewInstanceOf<Account>();
                _account.Id = 123;

                _venueMemberships = new[] {NewInstanceOf<VenueMembership>(), NewInstanceOf<VenueMembership>()};
                Injected<IVenueMembershipRepository>()
                    .Stub(r => r.GetMemberships(_account.Id))
                    .Return(_venueMemberships);
            };

            static Account _account;
            static IEnumerable<VenueMembership> _result;
            static IEnumerable<VenueMembership> _venueMemberships;
        }

        class When_getting_a_venue_membership_by_venue_id
        {
            Because of = () => _result = Subject.GetVenueMembership(_account, VenueId);

            It should_return_the_venue_membership_for_the_specified_user_and_venue = 
                () => _result.ShouldEqual(_venueMembership);

            Establish context = () =>
            {
                _account = NewInstanceOf<Account>();
                _account.Id = 456;

                _venueMembership = NewInstanceOf<VenueMembership>();
                Injected<IVenueMembershipRepository>()
                    .Stub(r => r.GetMembership(_account.Id, VenueId))
                    .Return(_venueMembership);
            };

            static VenueMembership _result;
            static Account _account;
            static VenueMembership _venueMembership;
            const int VenueId = 123;
        }

        class When_getting_venue_membership_by_place_id
        {
            Because of = () => _result = Subject.GetVenueMembership(_account, PublicPlaceId);

            It should_return_the_venue_membership_for_the_specified_user_and_place =
                () => _result.ShouldEqual(_venueMembership);

            Establish context = () =>
            {
                _account = NewInstanceOf<Account>();
                _account.Id = 456;

                _venueMembership = NewInstanceOf<VenueMembership>();
                Injected<IVenueMembershipRepository>()
                    .Stub(r => r.GetMembership(_account.Id, PublicPlaceId))
                    .Return(_venueMembership);
            };

            static VenueMembership _result;
            static Account _account;
            const string PublicPlaceId = "Public Place ID";
            static VenueMembership _venueMembership;
        }

        class When_getting_all_venue_memberships_for_a_venue_excluding_user_account
        {
            Because of = () => _result = Subject.GetAllVenueMemberships(VenueId, Skip, Take, _account);

            It should_return_the_venue_membership_results_collection_from_the_repository = () => _result.ShouldEqual(_venueMembershipsResultCollection);

            Establish context = () =>
            {
                _account = NewInstanceOf<Account>();
                _account.Id = 123;

                _venueMembershipsResultCollection = NewInstanceOf<IResultCollection<VenueMembership>>();

                Injected<IVenueMembershipRepository>()
                    .Stub(r => r.GetMemberships(VenueId, Skip, Take, _account.Id))
                    .Return(_venueMembershipsResultCollection);
            };

            static IResultCollection<VenueMembership> _result;
            const int VenueId = 456;
            const int Skip = 1;
            const int Take = 2;
            static Account _account;
            static IResultCollection<VenueMembership> _venueMembershipsResultCollection;
        }

        class When_getting_all_venue_memberships_for_a_venue
        {
            Because of = () => _result = Subject.GetAllVenueMemberships(VenueId, Skip, Take);

            It should_return_the_venue_membership_results_collection_from_the_repository = () => _result.ShouldEqual(_venueMembershipsResultCollection);

            Establish context = () =>
            {
                _account = NewInstanceOf<Account>();
                _account.Id = 123;

                _venueMembershipsResultCollection = NewInstanceOf<IResultCollection<VenueMembership>>();

                Injected<IVenueMembershipRepository>()
                    .Stub(r => r.GetMemberships(VenueId, Skip, Take, null))
                    .Return(_venueMembershipsResultCollection);
            };

            static IResultCollection<VenueMembership> _result;
            const int VenueId = 456;
            const int Skip = 1;
            const int Take = 2;
            static Account _account;
            static IResultCollection<VenueMembership> _venueMembershipsResultCollection;
        }
    }
}

