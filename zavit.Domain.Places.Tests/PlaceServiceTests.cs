﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Places.PublicPlaces;
using zavit.Domain.Places.Search;
using zavit.Domain.Places.Suggestions;
using zavit.Domain.Places.VenuePlaces;
using zavit.Domain.Places.VenuePlaces.DefaultVenues;
using zavit.Domain.Venues;
using zavit.Domain.Venues.NewVenueCreation;

namespace zavit.Domain.Places.Tests 
{
    [Subject("PlaceService")]
    public class PlaceServiceTests : TestOf<PlaceService>
    {
        class When_suggesting_places
        {
            Because of = () => _result = Subject.Suggest(_placeSearchCriteria).Result;

            It should_return_all_places_suggested_by_public_places_service = () => _result.ShouldEqual(_suggestedPlaces);

            Establish context = () =>
            {
                _placeSearchCriteria = NewInstanceOf<IPlaceSearchCriteria>();

                var publicPlaces = (IEnumerable<PublicPlace>) new[] { NewInstanceOf<PublicPlace>()};
                Injected<IPublicPlacesService>().Stub(p => p.GetPublicPlaces(_placeSearchCriteria)).Return(Task.FromResult(publicPlaces));

                var venuePlaces = (IEnumerable<VenuePlace>) new[] { NewInstanceOf<VenuePlace>() };
                Injected<IVenuePlaceRepository>().Stub(r => r.SearchPlaces(_placeSearchCriteria)).Return(Task.FromResult(venuePlaces));

                _suggestedPlaces = new[] { NewInstanceOf<IPlace>() };
                Injected<IPlaceSuggestionsMerger>().Stub(t => t.Merge(publicPlaces, venuePlaces)).Return(_suggestedPlaces);
            };

            static IPlaceSearchCriteria _placeSearchCriteria;
            static IEnumerable<IPlace> _result;
            static IEnumerable<IPlace> _suggestedPlaces;
        }

        class When_adding_a_new_venue_to_a_place_that_is_registered_as_a_venue_place
        {
            Because of = () => _result = Subject.AddVenue(_newVenue, PlaceId, _venueOwnerAccount).Result;

            It should_add_the_venue_to_the_place = () => _place.AssertWasCalled(p => p.AddVenue(_venue));

            It should_save_the_venue_place_in_the_repository =
                () => Injected<IVenuePlaceRepository>().AssertWasCalled(r => r.Update(_place));

            It should_return_the_new_venue = () => _result.ShouldEqual(_venue);

            Establish context = () =>
            {
                _venueOwnerAccount = NewInstanceOf<Account>();

                _newVenue = NewInstanceOf<NewVenue>();
                _venue = NewInstanceOf<Venue>();
                Injected<IVenueService>().Stub(s => s.CreateVenue(_newVenue, _venueOwnerAccount)).Return(_venue);

                _place = NewInstanceOf<VenuePlace>();
                Injected<IVenuePlaceRepository>().Stub(r => r.Get(PlaceId)).Return(_place);
            };

            static Venue _result;
            static NewVenue _newVenue;
            static Venue _venue;
            static VenuePlace _place;
            static Account _venueOwnerAccount;
            const string PlaceId = "Place ID";
        }

        class When_adding_a_new_venue_to_a_place_that_has_not_been_registered_as_a_venue_place
        {
            Because of = () => _result = Subject.AddVenue(_newVenue, PlaceId, _venueOwnerAccount).Result;

            It should_add_the_venue_to_the_place = () => _place.AssertWasCalled(p => p.AddVenue(_venue));

            It should_save_the_venue_place_in_the_repository =
                () => Injected<IVenuePlaceRepository>().AssertWasCalled(r => r.Save(_place));

            It should_return_the_new_venue = () => _result.ShouldEqual(_venue);

            Establish context = () =>
            {
                _venueOwnerAccount = NewInstanceOf<Account>();

                _newVenue = NewInstanceOf<NewVenue>();
                _venue = NewInstanceOf<Venue>();
                Injected<IVenueService>().Stub(s => s.CreateVenue(_newVenue, _venueOwnerAccount)).Return(_venue);

                _place = NewInstanceOf<VenuePlace>();
                Injected<IVenuePlaceCreator>().Stub(r => r.Create(PlaceId)).Return(Task.FromResult(_place));
            };

            static Venue _result;
            static NewVenue _newVenue;
            static Venue _venue;
            static VenuePlace _place;
            static Account _venueOwnerAccount;
            const string PlaceId = "Place ID";
        }

        class When_getting_a_default_venue_for_a_venue_place
        {
            Because of = () => _result = Subject.GetDefaultVenue(PlaceId).Result;

            It should_return_a_default_venue_created_for_a_venue_place = () => _result.ShouldEqual(_venue);

            Establish context = () =>
            {
                var venuePlace = NewInstanceOf<VenuePlace>();
                Injected<IVenuePlaceRepository>().Stub(r => r.Get(PlaceId)).Return(venuePlace);

                _venue = NewInstanceOf<Venue>();
                Injected<IDefaultVenueProvider>().Stub(s => s.ProvideDefaultVenue(venuePlace)).Return(_venue);
            };

            static Venue _result;
            static Venue _venue;
            const string PlaceId = "Place ID";
        }

        class When_getting_a_default_venue_for_a_public_place
        {
            Because of = () => _result = Subject.GetDefaultVenue(PlaceId).Result;

            It should_return_a_default_venue_created_for_a_public_place = () => _result.ShouldEqual(_venue);

            Establish context = () =>
            {
                var venuePlace = NewInstanceOf<VenuePlace>();
                Injected<IVenuePlaceRepository>().Stub(r => r.Get(PlaceId)).Return(null);

                var publicPlace = NewInstanceOf<PublicPlace>();
                Injected<IPublicPlacesService>()
                    .Stub(p => p.GetPublicPlace(PlaceId))
                    .Return(Task.FromResult(publicPlace));
                
                _venue = NewInstanceOf<Venue>();
                Injected<IDefaultVenueProvider>().Stub(s => s.ProvideDefaultVenue(publicPlace)).Return(_venue);
            };

            static Venue _result;
            static Venue _venue;
            const string PlaceId = "Place ID";
        }
    }
}

