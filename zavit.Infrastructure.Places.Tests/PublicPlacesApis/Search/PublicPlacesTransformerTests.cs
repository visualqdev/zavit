using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using Rhino.Mspec.Contrib.Extensions;
using zavit.Domain.Places.PublicPlaces;
using zavit.Infrastructure.Places.PublicPlacesApis.Search;

namespace zavit.Infrastructure.Places.Tests.PublicPlacesApis.Search 
{
    [Subject("PlaceSearchResultsTransformer")]
    public class PublicPlacesTransformerTests : TestOf<PlaceSearchResultsTransformer>
    {
        class When_transforming_google_place_search_result
        {
            Because of = () => _result = Subject.Transform(_googlePlacesSearchResult);

            It should_return_a_public_place_for_each_place_search_result = 
                () => _result.ShouldContainOnlyOrdered(_publicPlace, _otherPublicPlace);

            Establish context = () =>
            {
                _googlePlacesSearchResult = NewInstanceOf<GooglePlaceSearchResult>();

                var googlePlace = NewInstanceOf<GooglePlaceSearch>();
                var anotherGooglePlace = NewInstanceOf<GooglePlaceSearch>();

                _googlePlacesSearchResult.results = new [] {googlePlace, anotherGooglePlace};

                _publicPlace = NewInstanceOf<PublicPlace>();
                _otherPublicPlace = NewInstanceOf<PublicPlace>();

                Injected<IPlaceSearchTransformer>().Stub(p => p.Transform(googlePlace)).Return(_publicPlace);
                Injected<IPlaceSearchTransformer>().Stub(p => p.Transform(anotherGooglePlace)).Return(_otherPublicPlace);
            };

            static GooglePlaceSearchResult _googlePlacesSearchResult;
            static IEnumerable<PublicPlace> _result;
            static PublicPlace _publicPlace;
            static PublicPlace _otherPublicPlace;
        }
    }
}

