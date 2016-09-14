using System.Collections.Generic;
using System.Threading.Tasks;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Places.PublicPlaces;
using zavit.Domain.Places.Search;
using zavit.Infrastructure.Places.PublicPlacesApis;
using zavit.Infrastructure.Places.PublicPlacesApis.Details;
using zavit.Infrastructure.Places.PublicPlacesApis.Search;

namespace zavit.Infrastructure.Places.Tests 
{
    [Subject("PublicPlacesService")]
    public class PublicPlacesServiceTests : TestOf<PublicPlacesService>
    {
        class When_requesting_public_places
        {
            Because of = () => _result = Subject.GetPublicPlaces(_placeSearchCriteria).Result;

            It should_return_a_list_of_public_places = () => _result.ShouldEqual(_publicPlaces);

            Establish context = () =>
            {
                _placeSearchCriteria = NewInstanceOf<IPlaceSearchCriteria>();

                var googlePlacesSearchResult = NewInstanceOf<GooglePlaceSearchResult>();
                Injected<IGooglePlacesApi>()
                    .Stub(a => a.NearbySearch(_placeSearchCriteria, PublicPlacesService.Keywords))
                    .Return(Task.FromResult(googlePlacesSearchResult));

                _publicPlaces = new[] { NewInstanceOf<PublicPlace>(), NewInstanceOf<PublicPlace>() };
                Injected<IPlaceSearchResultsTransformer>().Stub(p => p.Transform(googlePlacesSearchResult)).Return(_publicPlaces);
            };

            static IPlaceSearchCriteria _placeSearchCriteria;
            static IEnumerable<PublicPlace> _result;
            static IEnumerable<PublicPlace> _publicPlaces;
        }

        class When_requesting_public_places_by_name
        {
            Because of = () => _result = Subject.GetPublicPlaces(_placeSearchCriteria).Result;

            It should_return_a_list_of_public_places_by_name = () => _result.ShouldEqual(_publicPlaces);

            Establish context = () =>
            {
                _placeSearchCriteria = NewInstanceOf<IPlaceSearchCriteria>();
                _placeSearchCriteria.Stub(x => x.Name).Return("Name");

                var googlePlacesSearchResult = NewInstanceOf<GooglePlaceSearchResult>();
                Injected<IGooglePlacesApi>()
                    .Stub(a => a.NearbySearchByName(_placeSearchCriteria, PublicPlacesService.Keywords))
                    .Return(Task.FromResult(googlePlacesSearchResult));

                _publicPlaces = new[] { NewInstanceOf<PublicPlace>(), NewInstanceOf<PublicPlace>() };
                Injected<IPlaceSearchResultsTransformer>().Stub(p => p.Transform(googlePlacesSearchResult)).Return(_publicPlaces);
            };

            static IPlaceSearchCriteria _placeSearchCriteria;
            static IEnumerable<PublicPlace> _result;
            static IEnumerable<PublicPlace> _publicPlaces;
        }

        class When_requesting_public_places_by_name_and_name_is_empty
        {
            Because of = () => _result = Subject.GetPublicPlaces(_placeSearchCriteria).Result;

            It should_return_a_list_of_public_places = () => _result.ShouldEqual(_publicPlaces);

            Establish context = () =>
            {
                _placeSearchCriteria = NewInstanceOf<IPlaceSearchCriteria>();
                _placeSearchCriteria.Stub(x => x.Name).Return("");

                var googlePlacesSearchResult = NewInstanceOf<GooglePlaceSearchResult>();
                Injected<IGooglePlacesApi>()
                    .Stub(a => a.NearbySearch(_placeSearchCriteria, PublicPlacesService.Keywords))
                    .Return(Task.FromResult(googlePlacesSearchResult));

                _publicPlaces = new[] { NewInstanceOf<PublicPlace>(), NewInstanceOf<PublicPlace>() };
                Injected<IPlaceSearchResultsTransformer>().Stub(p => p.Transform(googlePlacesSearchResult)).Return(_publicPlaces);
            };

            static IPlaceSearchCriteria _placeSearchCriteria;
            static IEnumerable<PublicPlace> _result;
            static IEnumerable<PublicPlace> _publicPlaces;
        }

        class When_getting_public_place_by_place_id
        {
            Because of = () => _result = Subject.GetPublicPlace(PlaceId).Result;

            It should_return_a_public_place_provided_by_google_places_api = () => _result.ShouldEqual(_publicPlace);

            Establish context = () =>
            {
                var googlePlacesDetailsResult = NewInstanceOf<GooglePlaceDetailsResult>();
                Injected<IGooglePlacesApi>().Stub(a => a.GetDetails(PlaceId)).Return(Task.FromResult(googlePlacesDetailsResult));

                _publicPlace = NewInstanceOf<PublicPlace>();
                Injected<IPlaceDetailsResultTransformer>()
                    .Stub(t => t.Transform(googlePlacesDetailsResult))
                    .Return(_publicPlace);
            };

            static PublicPlace _result;
            static PublicPlace _publicPlace;
            const string PlaceId = "Place ID";
        }
    }
}

