using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Web.Api.Controllers;
using zavit.Web.Api.Dtos.Places;

namespace zavit.Web.Api.Tests.Controllers 
{
    [Subject("PlacesController")]
    public class PlacesControllerTests : TestOf<PlacesController>
    {
        class When_getting_list_of_places
        {
            Because of = () => _result = Subject.Get();

            It should_return_a_list_of_places = () => _result.ShouldNotBeNull();

            Establish context = () => {};

            static IEnumerable<PlaceDto> _result;
        }
    }
}

