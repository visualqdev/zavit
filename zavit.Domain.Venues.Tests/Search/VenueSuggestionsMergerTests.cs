using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Venues.PublicPlaces;
using zavit.Domain.Venues.Search;

namespace zavit.Domain.Venues.Tests.Search 
{
    [Subject("VenueSuggestionsMerger")]
    public class VenueSuggestionsMergerTests : TestOf<VenueSuggestionsMerger>
    {
        class When_merging_public_places_and_venues
        {
            Because of = () => _result = Subject.Merge(_publicPlaces, _venues);

            It should_not_try_to_create_a_public_place_suggestion_for_a_public_place_that_has_a_public_place_id_matching_one_of_the_venues
                    = () => Injected<IPublicVenueSuggestionFactory>().AssertWasNotCalled(f => f.Create(_otherPublicPlace));

            It should_return_all_venue_places_and_only_public_place_suggestions_that_have_different_place_id_from_venue_places =
                () => _result.ShouldContainOnly(_venue, _publicPlaceSuggestion);

            Establish context = () =>
            {
                _venue = NewInstanceOf<Venue>();
                _venue.PublicPlaceId = "Venue Place ID";
                _venues = new[] { _venue };

                var publicPlace = NewInstanceOf<PublicPlace>();
                publicPlace.PlaceId = "Unique public place ID";
                _otherPublicPlace = NewInstanceOf<PublicPlace>();
                _otherPublicPlace.PlaceId = _venue.PublicPlaceId;
                _publicPlaces = new[] { publicPlace, _otherPublicPlace };

                _publicPlaceSuggestion = NewInstanceOf<IVenue>();
                Injected<IPublicVenueSuggestionFactory>()
                    .Stub(f => f.Create(publicPlace))
                    .Return(_publicPlaceSuggestion);
            };

            static IEnumerable<IVenue> _result;
            static IEnumerable<PublicPlace> _publicPlaces;
            static IEnumerable<Venue> _venues;
            static IVenue _publicPlaceSuggestion;
            static Venue _venue;
            static PublicPlace _otherPublicPlace;
        }
    }
}

