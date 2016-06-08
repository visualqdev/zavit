using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Places;
using zavit.Web.Api.DtoFactories.Places;
using zavit.Web.Api.Dtos.Places;

namespace zavit.Web.Api.Tests.DtoFactories.Places
{
    [Subject("PlaceDtoFactory")]
    public class PlaceDtoFactoryTests : TestOf<PlaceDtoFactory>
    {
        class When_creating_a_place_dto
        {
            Because of = () => _result = Subject.CreateItem(_place);

            It should_set_the_longitude_to_be_the_same_as_that_of_the_place = () => _result.Longitude.ShouldEqual(_place.Longitude);
            It should_set_the_latitude_to_be_the_same_as_that_of_the_place = () => _result.Latitude.ShouldEqual(_place.Latitude);
            It should_set_the_place_id_to_be_the_same_as_that_of_the_place = () => _result.PlaceId.ShouldEqual(_place.PlaceId);

            Establish context = () =>
            {
                _place = NewInstanceOf<IPlace>();
                _place.Stub(p => p.Longitude).Return(0.0003);
                _place.Stub(p => p.Latitude).Return(1.222);
                _place.Stub(p => p.PlaceId).Return("fdsa56");
            };

            static IPlace _place;
            static PlaceDto _result;
        }
    }
}