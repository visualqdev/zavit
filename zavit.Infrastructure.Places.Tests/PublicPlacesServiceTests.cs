using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Places.PublicPlaces;
using zavit.Domain.Places.Search;
using zavit.Infrastructure.Places.PublicPlacesApis;

namespace zavit.Infrastructure.Places.Tests 
{
    [Subject("PublicPlacesService")]
    public class PublicPlacesServiceTests : TestOf<PublicPlacesService>
    {
        class When_requesting_public_places
        {
            Because of = () => _result = Subject.GetPublicPlaces(_placeSearchCriteria);

            It should_return_a_list_of_public_places = () => _result.ShouldEqual(_publicPlaces);

            Establish context = () =>
            {
                _placeSearchCriteria = NewInstanceOf<IPlaceSearchCriteria>();

                var googlePlacesSearchResult = NewInstanceOf<GooglePlacesSearchResult>();
                Injected<IGooglePlaceSearchApi>()
                    .Stub(a => a.NearbySearch(_placeSearchCriteria))
                    .Return(googlePlacesSearchResult);

                _publicPlaces = new[] { NewInstanceOf<PublicPlace>(), NewInstanceOf<PublicPlace>() };
                Injected<IPublicPlacesTransformer>().Stub(p => p.Transform(googlePlacesSearchResult)).Return(_publicPlaces);
            };

            static IPlaceSearchCriteria _placeSearchCriteria;
            static IEnumerable<PublicPlace> _result;
            static IEnumerable<PublicPlace> _publicPlaces;
        }
    }
}

