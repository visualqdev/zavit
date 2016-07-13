using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mspec.Contrib;
using zavit.Domain.Places.VenuePlaces;
using zavit.Domain.Venues;

namespace zavit.Domain.Places.Tests.VenuePlaces 
{
    [Subject("VenuePlace")]
    public class VenuePlaceTests : TestOf<VenuePlace>
    {
        class When_adding_a_venue_place
        {
            Because of = () => Subject.AddVenue(_venue);

            It should_add_the_venue_to_the_list_of_venues = () => Subject.Venues.ShouldContainOnly(_existingVenue, _venue);

            Establish context = () =>
            {
                _existingVenue = NewInstanceOf<Venue>();
                Subject.Venues = new List<Venue> { _existingVenue };
                _venue = NewInstanceOf<Venue>();
            };

            static Venue _existingVenue;
            static Venue _venue;
        }
    }
}

