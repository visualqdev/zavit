using System.Collections.Generic;
using System.Threading.Tasks;
using zavit.Domain.Accounts;
using zavit.Domain.Places.PublicPlaces;
using zavit.Domain.Places.Search;
using zavit.Domain.Places.Suggestions;
using zavit.Domain.Places.VenuePlaces;
using zavit.Domain.Venues;
using zavit.Domain.Venues.NewVenueCreation;

namespace zavit.Domain.Places
{
    public class PlaceService : IPlaceService
    {
        readonly IPublicPlacesService _publicPlacesService;
        readonly IVenuePlaceRepository _venuePlaceRepository;
        readonly IVenueService _venueService;
        readonly IVenuePlaceCreator _venuePlaceCreator;
        readonly IPlaceSuggestionsMerger _placeSuggestionsMerger;

        public PlaceService(IPublicPlacesService publicPlacesService, IVenuePlaceRepository venuePlaceRepository, IVenueService venueService, IVenuePlaceCreator venuePlaceCreator, IPlaceSuggestionsMerger placeSuggestionsMerger)
        {
            _publicPlacesService = publicPlacesService;
            _venuePlaceRepository = venuePlaceRepository;
            _venueService = venueService;
            _venuePlaceCreator = venuePlaceCreator;
            _placeSuggestionsMerger = placeSuggestionsMerger;
        }

        public async Task<IEnumerable<IPlace>> Suggest(IPlaceSearchCriteria placeSearchCriteria)
        {
            var publicPlacesTask = _publicPlacesService.GetPublicPlaces(placeSearchCriteria);
            var venuePlacesTask = _venuePlaceRepository.SearchPlaces(placeSearchCriteria);

            await Task.WhenAll(publicPlacesTask, venuePlacesTask);

            var placeSuggestions = _placeSuggestionsMerger.Merge(publicPlacesTask.Result, venuePlacesTask.Result);

            return placeSuggestions;
        }

        public async Task<Venue> AddVenue(INewVenue newVenue, string placeId, Account venueOwnerAccount)
        {
            var place = _venuePlaceRepository.Get(placeId);

            var isNewPlace = false;
            if (place == null)
            {
                isNewPlace = true;
                place = await _venuePlaceCreator.Create(placeId);
            }

            var venue = _venueService.CreateVenue(newVenue, venueOwnerAccount);
            place.AddVenue(venue);

            if (isNewPlace)
                _venuePlaceRepository.Save(place);
            else
                _venuePlaceRepository.Update(place);

            return venue;
        }

        public async Task<IEnumerable<IPlace>> SuggestByName(IPlaceSearchByNameCriteria placeSearchByNameCriteria)
        {
            var publicPlacesTask = _publicPlacesService.GetPublicPlacesByName(placeSearchByNameCriteria);
            //var venuePlacesTask = _venuePlaceRepository.SearchPlacesByName(placeSearchByNameCriteria);

            await Task.WhenAll(publicPlacesTask/*, venuePlacesTask*/);

            var placeSuggestions = _placeSuggestionsMerger.Merge(publicPlacesTask.Result,/*, venuePlacesTask.Result*/null);

            return placeSuggestions;
        }
    }
}