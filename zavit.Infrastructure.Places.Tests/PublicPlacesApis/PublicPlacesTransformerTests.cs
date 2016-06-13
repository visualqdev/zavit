using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using Rhino.Mspec.Contrib.Extensions;
using zavit.Domain.Places.PublicPlaces;
using zavit.Infrastructure.Places.PublicPlacesApis;

namespace zavit.Infrastructure.Places.Tests.PublicPlacesApis 
{
    [Subject("PublicPlacesTransformer")]
    public class PublicPlacesTransformerTests : TestOf<PublicPlacesTransformer>
    {
        class When_transforming_google_place_search_result
        {
            Because of = () => _result = Subject.Transform(_googlePlacesSearchResult);

            It should_return_a_public_place_for_each_place_search_result = 
                () => _result.ShouldContainOnlyOrdered(_publicPlace, _otherPublicPlace);

            Establish context = () =>
            {
                _googlePlacesSearchResult = NewInstanceOf<GooglePlacesSearchResult>();

                var googlePlace = NewInstanceOf<GooglePlace>();
                var anotherGooglePlace = NewInstanceOf<GooglePlace>();

                //var anotherGooglePlace = new GooglePlace
                //{
                //    place_id = "id2",
                //    geometry = new GoogleGeometry
                //    {
                //        location = new GoogleLocation { lat = 0.1, lng = 0.2 }
                //    }
                //};

                _googlePlacesSearchResult.results = new [] {googlePlace, anotherGooglePlace};

                _publicPlace = NewInstanceOf<PublicPlace>();
                _otherPublicPlace = NewInstanceOf<PublicPlace>();

                Injected<IPublicPlaceTransformer>().Stub(p => p.Transform(googlePlace)).Return(_publicPlace);
                Injected<IPublicPlaceTransformer>().Stub(p => p.Transform(anotherGooglePlace)).Return(_otherPublicPlace);
            };

            static GooglePlacesSearchResult _googlePlacesSearchResult;
            static IEnumerable<PublicPlace> _result;
            static PublicPlace _publicPlace;
            static PublicPlace _otherPublicPlace;
        }
    }
}

