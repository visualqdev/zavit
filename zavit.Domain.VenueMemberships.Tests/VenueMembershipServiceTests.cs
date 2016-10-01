using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
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

        class When_trying_to_add_a_user_to_the_venue_but_the_user_is_already_a_member
        {
            Because of = () => _result = Subject.AddUserToVenue(_account, _newVenueMembership);

            It should_return_the_exisitng_venue_membership = () => _result.ShouldEqual(_venueMembership);

            Establish context = () =>
            {
                _account = NewInstanceOf<Account>();
                _account.Id = 123;

                _newVenueMembership = NewInstanceOf<NewVenueMembership>();
                _newVenueMembership.VenueId = 456;

                _venueMembership = NewInstanceOf<VenueMembership>();
                Injected<IVenueMembershipRepository>()
                    .Stub(r => r.GetMembership(_account.Id, _newVenueMembership.VenueId))
                    .Return(_venueMembership);
            };

            static Account _account;
            static NewVenueMembership _newVenueMembership;
            static VenueMembership _result;
            static VenueMembership _venueMembership;
        }

        class When_getting_venue_memberships
        {
            Because of = () => _result = Subject.GetVenueMemberships(_account);

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
    }
}

