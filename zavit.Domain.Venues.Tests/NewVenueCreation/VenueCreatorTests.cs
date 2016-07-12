using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Venues.NewVenueCreation;

namespace zavit.Domain.Venues.Tests.NewVenueCreation 
{
    [Subject("VenueCreator")]
    public class VenueCreatorTests : TestOf<VenueCreator>
    {
        class When_creating_a_new_venue
        {
            Because of = () => _result = Subject.Create(_newVenue);

            It should_set_the_venue_name_to_be_the_same_as_the_name_specified_in_new_venue_request = 
                () => _result.Name.ShouldEqual(_newVenue.Name);

            Establish context = () =>
            {
                _newVenue = NewInstanceOf<INewVenue>();
                _newVenue.Stub(v => v.Name).Return("New venue name");
            };

            static INewVenue _newVenue;
            static Venue _result;
        }
    }
}

