using System.Collections.Generic;
using System.Threading.Tasks;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Web.Api.Controllers;
using zavit.Web.Api.Dtos.Places;
using zavit.Web.Api.DtoServices.Places;

namespace zavit.Web.Api.Tests.Controllers 
{
    [Subject("PlacesController")]
    public class PlacesControllerTests : TestOf<PlacesController>
    {
        class When_getting_list_of_places
        {
            Because of = () => _result = Subject.Get(_placeSearchCriteriaDto).Result;

            It should_return_a_list_of_places = () => _result.ShouldNotBeNull();

            Establish context = () =>
            {
                _placeSearchCriteriaDto = NewInstanceOf<PlaceSearchCriteriaDto>();

                _places = new[] { NewInstanceOf<PlaceDto>() };
                Injected<IPlaceDtoService>().Stub(s => s.SuggestPlaces(_placeSearchCriteriaDto)).Return(Task.FromResult(_places));
            };

            static IEnumerable<PlaceDto> _result;
            static IEnumerable<PlaceDto> _places;
            static PlaceSearchCriteriaDto _placeSearchCriteriaDto;
        }
    }
}

