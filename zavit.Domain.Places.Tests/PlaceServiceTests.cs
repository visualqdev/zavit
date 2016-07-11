using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Places.PublicPlaces;
using zavit.Domain.Places.Search;
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

        class When_adding_a_new_venue
        {
            Because of = () => _result = Subject.AddVenue(_newVenue, PlaceId);

            It should_ = () => ;

            Establish context = () => {};

            static Venue _result;
            static INewVenue _newVenue;
            const string PlaceId = "Place ID";
        }
    }
}

