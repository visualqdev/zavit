using Machine.Specifications;
using Rhino.Mspec.Contrib;
using zavit.Domain.Venues.PublicPlaces;
using zavit.Domain.Venues.Search;

namespace zavit.Domain.Venues.Tests.Search 
{
    [Subject("PublicVenueSuggestionFactory")]
    public class PublicVenueSuggestionFactoryTests : TestOf<PublicVenueSuggestionFactory>
    {
        class When_creating_a_public_venue_suggestion
        {
            Because of = () => _result = Subject.Create(_publicPlace);

            It should_return_an_instance_of_public_place_suggestion =
                () => _result.ShouldBeAssignableTo<PublicVenueSuggestion>();

            It should_set_the_public_place_to_be_the_public_place_provided =
                () => ((PublicVenueSuggestion)_result).PublicPlace.ShouldEqual(_publicPlace);

            Establish context = () =>
            {
                _publicPlace = NewInstanceOf<PublicPlace>();
            };

            static PublicPlace _publicPlace;
            static IVenue _result;
        }
    }
}

