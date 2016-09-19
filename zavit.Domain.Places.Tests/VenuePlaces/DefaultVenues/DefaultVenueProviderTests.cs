using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Activities;
using zavit.Domain.Places.PublicPlaces;
using zavit.Domain.Places.VenuePlaces;
using zavit.Domain.Places.VenuePlaces.DefaultVenues;
using zavit.Domain.Venues;

namespace zavit.Domain.Places.Tests.VenuePlaces.DefaultVenues 
{
    [Subject("DefaultVenueProvider")]
    public class DefaultVenueProviderTests : TestOf<DefaultVenueProvider>
    {
        class When_providing_a_default_venue_for_a_venue_place
        {
            Because of = () => _result = Subject.ProvideDefaultVenue(_venuePlace);

            It should_set_the_venue_name_to_be_the_same_as_the_name_of_the_place = 
                () => _result.Name.ShouldEqual(_venuePlace.Name);

            It should_set_the_venue_activities_to_be_default_activities = 
                () => _result.Activities.ShouldEqual(_activities);

            It should_set_the_venue_address_to_be_the_venue_place_address =
                () => _result.Address.ShouldEqual(_venuePlace.Address);

            Establish context = () =>
            {
                _venuePlace = NewInstanceOf<VenuePlace>();
                _venuePlace.Name = "Place name";
                _venuePlace.Address = "Test address";

                _activities = new[] { NewInstanceOf<Activity>() };
                Injected<IActivityRepository>().Stub(r => r.GetDefaultActivities()).Return(_activities);
            };

            static IList<Activity> _activities;
            static VenuePlace _venuePlace;
            static Venue _result;
        }

        class When_providing_a_default_venue_for_a_public_place
        {
            Because of = () => _result = Subject.ProvideDefaultVenue(_publicPlace);

            It should_set_the_venue_name_to_be_the_same_as_the_name_of_the_place =
                () => _result.Name.ShouldEqual(_publicPlace.Name);

            It should_set_the_venue_activities_to_be_default_activities =
                () => _result.Activities.ShouldEqual(_activities);

            It should_set_the_venue_address_to_be_the_public_place_address =
                () => _result.Address.ShouldEqual(_publicPlace.Address);

            Establish context = () =>
            {
                _publicPlace = NewInstanceOf<PublicPlace>();
                _publicPlace.Name = "Place name";
                _publicPlace.Address = "Test address";

                _activities = new[] { NewInstanceOf<Activity>() };
                Injected<IActivityRepository>().Stub(r => r.GetDefaultActivities()).Return(_activities);
            };

            static IList<Activity> _activities;
            static PublicPlace _publicPlace;
            static Venue _result;
        }
    }
}

