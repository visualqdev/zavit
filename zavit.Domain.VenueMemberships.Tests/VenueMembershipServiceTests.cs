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
    }
}

