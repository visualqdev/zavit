using System.Collections.Generic;
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

