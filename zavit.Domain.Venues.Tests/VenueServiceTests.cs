using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Venues.NewVenueCreation;

namespace zavit.Domain.Venues.Tests 
{
    [Subject("VenueService")]
    public class VenueServiceTests : TestOf<VenueService>
    {
        class When_creating_a_venue
        {
            Because of = () => _result = Subject.CreateVenue(_newVenue, _venueOwnerAccount);

            It should_return_a_newly_created_venue = () => _result.ShouldEqual(_venue);

            Establish context = () =>
            {
                _venueOwnerAccount = NewInstanceOf<Account>();

                _newVenue = NewInstanceOf<NewVenue>();

                _venue = NewInstanceOf<Venue>();
                Injected<IVenueCreator>().Stub(v => v.Create(_newVenue, _venueOwnerAccount)).Return(_venue);
            };

            static NewVenue _newVenue;
            static Venue _result;
            static Venue _venue;
            static Account _venueOwnerAccount;
        }
    }
}

