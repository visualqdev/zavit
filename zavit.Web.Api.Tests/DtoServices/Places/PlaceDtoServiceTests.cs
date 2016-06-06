using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using Rhino.Mspec.Contrib.Extensions;
using zavit.Domain.Places;
using zavit.Web.Api.DtoFactories.Places;
using zavit.Web.Api.Dtos.Places;
using zavit.Web.Api.DtoServices.Places;

namespace zavit.Web.Api.Tests.DtoServices.Places 
{
    [Subject("PlaceDtoService")]
    public class PlaceDtoServiceTests : TestOf<PlaceDtoService>
    {
        class When_providing_suggesting_place_dtos
        {
            Because of = () => _result = Subject.SuggestPlaces();

            It should_return_a_dto_for_each_of_the_places_suggested_by_the_place_service = () => _result.ShouldContainOnlyOrdered(_placeDto, _otherPlaceDto);

            Establish context = () =>
            {
                var place = NewInstanceOf<IPlace>();
                var otherPlace = NewInstanceOf<IPlace>();

                Injected<IPlaceService>().Stub(s => s.Suggest()).Return(new[] { place, otherPlace });

                _placeDto = NewInstanceOf<PlaceDto>();
                Injected<IPlaceDtoFactory>().Stub(f => f.CreateItem(place)).Return(_placeDto);

                _otherPlaceDto = NewInstanceOf<PlaceDto>();
                Injected<IPlaceDtoFactory>().Stub(f => f.CreateItem(otherPlace)).Return(_otherPlaceDto);
            };

            static IEnumerable<PlaceDto> _result;

            static PlaceDto _placeDto;
            static PlaceDto _otherPlaceDto;
        }
    }
}

