using Machine.Specifications;
using Rhino.Mspec.Contrib;
using zavit.Domain.Places.PublicPlaces;
using zavit.Domain.Places.Suggestions;

namespace zavit.Domain.Places.Tests.Suggestions 
{
    [Subject("PublicPlaceSuggestionFactory")]
    public class PublicPlaceSuggestionFactoryTests : TestOf<PublicPlaceSuggestionFactory>
    {
        class When_creating_a_public_place_suggestion
        {
            Because of = () => _result = Subject.Create(_publicPlace);

            It should_return_an_instance_of_public_place_suggestion =
                () => _result.ShouldBeAssignableTo<PublicPlaceSuggestion>();

            It should_set_the_public_place_to_be_the_public_place_provided =
                () => ((PublicPlaceSuggestion) _result).PublicPlace.ShouldEqual(_publicPlace);

            Establish context = () =>
            {
                _publicPlace = NewInstanceOf<PublicPlace>();
            };

            static PublicPlace _publicPlace;
            static IPlace _result;
        }
    }
}

