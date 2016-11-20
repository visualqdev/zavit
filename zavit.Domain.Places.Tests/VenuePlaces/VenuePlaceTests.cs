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
        class When_adding_a_venue_that_has_no_name_set
        {
            Because of = () => Subject.AddVenue(_venue);

            It should_add_the_venue_to_the_list_of_venues = () => Subject.Venues.ShouldContainOnly(_existingVenue, _venue);

            It should_set_venue_address_to_be_the_venue_place_address =
                () => _venue.Address.ShouldEqual(Subject.Address);

            It should_set_the_venue_name_to_be_the_same_as_the_name_of_the_venue_place =
                () => _venue.Name.ShouldEqual(Subject.Name);

            It should_set_the_venue_longitude_to_be_the_same_as_place_logitude =
                () => _venue.Longitude.ShouldEqual(Subject.Longitude);

            It should_set_the_venue_latitude_to_be_the_same_as_place_latitude =
                () => _venue.Latitude.ShouldEqual(Subject.Latitude);

            Establish context = () =>
            {
                _existingVenue = NewInstanceOf<Venue>();
                Subject.Venues = new List<Venue> { _existingVenue };
                _venue = NewInstanceOf<Venue>();

                Subject.Name = "Test place name";
                Subject.Address = "Test place address";
                Subject.Longitude = 0.1;
                Subject.Latitude = -0.2;
            };

            static Venue _existingVenue;
            static Venue _venue;
        }

        class When_adding_a_venue_that_has_a_name_set
        {
            Because of = () => Subject.AddVenue(_venue);

            It should_add_the_venue_to_the_list_of_venues = () => Subject.Venues.ShouldContainOnly(_existingVenue, _venue);

            It should_set_venue_address_to_be_the_venue_place_address =
                () => _venue.Address.ShouldEqual(Subject.Address);

            It should_not_try_to_change_the_venue_name =
                () => _venue.Name.ShouldEqual(VenueName);

            It should_set_the_venue_longitude_to_be_the_same_as_place_logitude =
                () => _venue.Longitude.ShouldEqual(Subject.Longitude);

            It should_set_the_venue_latitude_to_be_the_same_as_place_latitude =
                () => _venue.Latitude.ShouldEqual(Subject.Latitude);

            Establish context = () =>
            {
                _existingVenue = NewInstanceOf<Venue>();
                Subject.Venues = new List<Venue> { _existingVenue };
                _venue = NewInstanceOf<Venue>();
                _venue.Name = VenueName;

                Subject.Name = "Test place name";
                Subject.Address = "Test place address";
                Subject.Longitude = 0.1;
                Subject.Latitude = -0.2;
            };

            static Venue _existingVenue;
            static Venue _venue;
            const string VenueName = "Venue test name";
        }
    }
}

