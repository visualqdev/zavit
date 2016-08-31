using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Places.PublicPlaces;
using zavit.Domain.Places.Suggestions;
using zavit.Domain.Places.VenuePlaces;

namespace zavit.Domain.Places.Tests.Suggestions 
{
    [Subject("PlaceSuggestionsMerger")]
    public class PlaceSuggestionsMergerTests : TestOf<PlaceSuggestionsMerger>
    {
        class When_merging_public_places_and_venue_places
        {
            Because of = () => _result = Subject.Merge(_publicPlaces, _venuePlaces);

            It should_not_try_to_create_a_public_place_suggestion_for_a_public_place_that_has_a_place_id_matching_one_of_the_venue_places
                    = () => Injected<IPublicPlaceSuggestionFactory>().AssertWasNotCalled(f => f.Create(_otherPublicPlace));

            It should_return_all_venue_places_and_only_public_place_suggestions_that_have_different_place_id_from_venue_places = 
                () => _result.ShouldContainOnly(_venuePlace, _publicPlaceSuggestion);

            Establish context = () =>
            {
                _venuePlace = NewInstanceOf<VenuePlace>();
                _venuePlace.PlaceId = "Venue Place ID";
                _venuePlaces = new[] { _venuePlace };

                var publicPlace = NewInstanceOf<PublicPlace>();
                publicPlace.PlaceId = "Unique public place ID";
                _otherPublicPlace = NewInstanceOf<PublicPlace>();
                _otherPublicPlace.PlaceId = _venuePlace.PlaceId;
                _publicPlaces = new[] { publicPlace, _otherPublicPlace };

                _publicPlaceSuggestion = NewInstanceOf<IPlace>();
                Injected<IPublicPlaceSuggestionFactory>()
                    .Stub(f => f.Create(publicPlace))
                    .Return(_publicPlaceSuggestion);
            };

            static IEnumerable<IPlace> _result;
            static IEnumerable<PublicPlace> _publicPlaces;
            static IEnumerable<VenuePlace> _venuePlaces;
            static IPlace _publicPlaceSuggestion;
            static VenuePlace _venuePlace;
            static PublicPlace _otherPublicPlace;
        }
    }
}

