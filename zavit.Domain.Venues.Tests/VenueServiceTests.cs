using System.Collections.Generic;
using System.Threading.Tasks;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Venues.DefaultVenues;
using zavit.Domain.Venues.NewVenueCreation;
using zavit.Domain.Venues.PublicPlaces;
using zavit.Domain.Venues.Search;

namespace zavit.Domain.Venues.Tests 
{
    [Subject("VenueService")]
    public class VenueServiceTests : TestOf<VenueService>
    {
        class When_creating_a_new_venue_at_a_public_place
        {
            Because of = () => _result = Subject.CreateVenue(_newVenue, _venueOwnerAccount).Result;

            It should_save_the_venue_in_the_repository =
                () => Injected<IVenueRepository>().AssertWasCalled(r => r.Save(_venue));

            It should_return_the_new_venue = () => _result.ShouldEqual(_venue);

            Establish context = () =>
            {
                _venueOwnerAccount = NewInstanceOf<Account>();

                _newVenue = NewInstanceOf<NewVenue>();
                _newVenue.PublicPlaceId = "Place ID";

                _venue = NewInstanceOf<Venue>();
                Injected<IVenueCreator>().Stub(r => r.Create(_newVenue, _venueOwnerAccount)).Return(Task.FromResult(_venue));
            };

            static Venue _result;
            static NewVenue _newVenue;
            static Venue _venue;
            static Account _venueOwnerAccount;
        }

        class When_creating_a_new_venue_at_a_public_place_but_there_already_is_a_venue
        {
            Because of = () => _result = Subject.CreateVenue(_newVenue, _venueOwnerAccount).Result;

            It should_not_try_to_save_any_venue =
                () => Injected<IVenueRepository>().AssertWasNotCalled(r => r.Save(Arg<Venue>.Is.Anything));

            It should_return_the_existing_venue = () => _result.ShouldEqual(_venue);

            Establish context = () =>
            {
                _venueOwnerAccount = NewInstanceOf<Account>();

                _newVenue = NewInstanceOf<NewVenue>();
                _newVenue.PublicPlaceId = "Place ID";

                _venue = NewInstanceOf<Venue>();
                Injected<IVenueRepository>().Stub(r => r.GetVenue(_newVenue.PublicPlaceId)).Return(_venue);
            };

            static Venue _result;
            static NewVenue _newVenue;
            static Venue _venue;
            static Account _venueOwnerAccount;
        }

        class When_suggesting_venues
        {
            Because of = () => _result = Subject.SuggestVenues(_venueSearchCriteria).Result;

            It should_return_all_venues_suggested_by_public_places_service_merged_with_registered_venues = 
                () => _result.ShouldEqual(_suggestedVenues);

            Establish context = () =>
            {
                _venueSearchCriteria = NewInstanceOf<IVenueSearchCriteria>();

                var publicPlaces = (IEnumerable<PublicPlace>)new[] { NewInstanceOf<PublicPlace>() };
                Injected<IPublicPlacesService>().Stub(p => p.GetPublicPlaces(_venueSearchCriteria)).Return(Task.FromResult(publicPlaces));

                var venues = (IEnumerable<Venue>)new[] { NewInstanceOf<Venue>() };
                Injected<IVenueRepository>().Stub(r => r.SearchVenues(_venueSearchCriteria)).Return(Task.FromResult(venues));

                _suggestedVenues = new[] { NewInstanceOf<IVenue>() };
                Injected<IVenueSuggestionsMerger>().Stub(t => t.Merge(publicPlaces, venues)).Return(_suggestedVenues);
            };

            static IVenueSearchCriteria _venueSearchCriteria;
            static IEnumerable<IVenue> _result;
            static IEnumerable<IVenue> _suggestedVenues;
        }

        class When_getting_a_default_venue_for_a_public_place
        {
            Because of = () => _result = Subject.GetDefaultVenue(PlaceId).Result;

            It should_return_a_default_venue_created_for_a_public_place = () => _result.ShouldEqual(_venue);

            Establish context = () =>
            {
                Injected<IVenueRepository>().Stub(r => r.GetVenue(PlaceId)).Return(null);

                var publicPlace = NewInstanceOf<PublicPlace>();
                Injected<IPublicPlacesService>()
                    .Stub(p => p.GetPublicPlace(PlaceId))
                    .Return(Task.FromResult(publicPlace));

                _venue = NewInstanceOf<Venue>();
                Injected<IDefaultVenueProvider>().Stub(s => s.ProvideDefaultVenue(publicPlace)).Return(_venue);
            };

            static Venue _result;
            static Venue _venue;
            const string PlaceId = "Place ID";
        }

        class When_getting_a_default_venue_but_there_already_is_a_venue_at_the_specified_public_place
        {
            Because of = () => _result = Subject.GetDefaultVenue(PlaceId).Result;

            It should_return_the_existing_venue = () => _result.ShouldEqual(_venue);

            Establish context = () =>
            {
                _venue = NewInstanceOf<Venue>();
                Injected<IVenueRepository>().Stub(r => r.GetVenue(PlaceId)).Return(_venue);
            };

            static Venue _result;
            static Venue _venue;
            const string PlaceId = "Place ID";
        }
    }
}

