using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Venues.NewVenueCreation;

namespace zavit.Domain.Venues.Tests 
{
    [Subject("VenueService")]
    public class VenueServiceTests : TestOf<VenueService>
    {
        class When_creating_a_venue
        {
            Because of = () => _result = Subject.CreateVenue(_newVenue);

            It should_return_a_newly_created_venue = () => _result.ShouldEqual(_venue);

            Establish context = () =>
            {
                _newVenue = NewInstanceOf<INewVenue>();

                _venue = NewInstanceOf<Venue>();
                Injected<IVenueCreator>().Stub(v => v.Create(_newVenue)).Return(_venue);
            };

            static INewVenue _newVenue;
            static Venue _result;
            static Venue _venue;
        }
    }
}

