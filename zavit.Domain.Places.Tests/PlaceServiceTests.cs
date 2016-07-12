using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Places.PublicPlaces;
using zavit.Domain.Places.Search;
using zavit.Domain.Places.VenuePlaces;
using zavit.Domain.Venues;
using zavit.Domain.Venues.NewVenueCreation;

namespace zavit.Domain.Places.Tests 
{
    [Subject("PlaceService")]
    public class PlaceServiceTests : TestOf<PlaceService>
    {
        class When_suggesting_places
        {
            Because of = () => _result = Subject.Suggest(_placeSearchCriteria);

            It should_return_all_places_suggested_by_public_places_service = () => _result.ShouldEqual(_publicPlaces);

            Establish context = () =>
            {
                _placeSearchCriteria = NewInstanceOf<IPlaceSearchCriteria>();
                
                _publicPlaces = new[] { NewInstanceOf<PublicPlace>() };
                Injected<IPublicPlacesService>().Stub(p => p.GetPublicPlaces(_placeSearchCriteria)).Return(_publicPlaces);
            };

            static IPlaceSearchCriteria _placeSearchCriteria;
            static IEnumerable<IPlace> _result;
            static IEnumerable<PublicPlace> _publicPlaces;
        }

        class When_adding_a_new_venue_to_a_place_that_is_registered_as_a_venue_place
        {
            Because of = () => _result = Subject.AddVenue(_newVenue, PlaceId);

            It should_add_the_venue_to_the_place = () => _place.AssertWasCalled(p => p.AddVenue(_venue));

            It should_save_the_venue_place_in_the_repository =
                () => Injected<IVenuePlaceRepository>().AssertWasCalled(r => r.Save(_place));

            It should_return_the_new_venue = () => _result.ShouldEqual(_venue);

            Establish context = () =>
            {
                _newVenue = NewInstanceOf<INewVenue>();
                _venue = NewInstanceOf<Venue>();
                Injected<IVenueService>().Stub(s => s.CreateVenue(_newVenue)).Return(_venue);

                _place = NewInstanceOf<VenuePlace>();
                Injected<IVenuePlaceRepository>().Stub(r => r.Get(PlaceId)).Return(_place);
            };

            static Venue _result;
            static INewVenue _newVenue;
            static Venue _venue;
            static VenuePlace _place;
            const string PlaceId = "Place ID";
        }

        class When_adding_a_new_venue_to_a_place_that_has_not_been_registered_as_a_venue_place
        {
            Because of = () => _result = Subject.AddVenue(_newVenue, PlaceId);

            It should_add_the_venue_to_the_place = () => _place.AssertWasCalled(p => p.AddVenue(_venue));

            It should_save_the_venue_place_in_the_repository =
                () => Injected<IVenuePlaceRepository>().AssertWasCalled(r => r.Save(_place));

            It should_return_the_new_venue = () => _result.ShouldEqual(_venue);

            Establish context = () =>
            {
                _newVenue = NewInstanceOf<INewVenue>();
                _venue = NewInstanceOf<Venue>();
                Injected<IVenueService>().Stub(s => s.CreateVenue(_newVenue)).Return(_venue);

                _place = NewInstanceOf<VenuePlace>();
                Injected<IVenuePlaceCreator>().Stub(r => r.Create(PlaceId)).Return(_place);
            };

            static Venue _result;
            static INewVenue _newVenue;
            static Venue _venue;
            static VenuePlace _place;
            const string PlaceId = "Place ID";
        }
    }
}

