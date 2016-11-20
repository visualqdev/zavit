using System.Collections.Generic;
using System.Threading.Tasks;
using zavit.Domain.Accounts;
using zavit.Domain.Venues.DefaultVenues;
using zavit.Domain.Venues.NewVenueCreation;
using zavit.Domain.Venues.PublicPlaces;
using zavit.Domain.Venues.Search;

namespace zavit.Domain.Venues
{
    public class VenueService : IVenueService
    {
        readonly IVenueCreator _venueCreator;
        readonly IPublicPlacesService _publicPlacesService;
        readonly IVenueRepository _venueRepository;
        readonly IVenueSuggestionsMerger _venueSuggestionsMerger;
        readonly IDefaultVenueProvider _defaultVenueProvider;

        public VenueService(IVenueCreator venueCreator, IPublicPlacesService publicPlacesService, IVenueRepository venueRepository, IVenueSuggestionsMerger venueSuggestionsMerger, IDefaultVenueProvider defaultVenueProvider)
        {
            _venueCreator = venueCreator;
            _publicPlacesService = publicPlacesService;
            _venueRepository = venueRepository;
            _venueSuggestionsMerger = venueSuggestionsMerger;
            _defaultVenueProvider = defaultVenueProvider;
        }

        public async Task<Venue> CreateVenue(NewVenue newVenue, Account venueOwnerAccount)
        {
            var venue = _venueRepository.GetVenue(newVenue.PublicPlaceId);
            if (venue != null) return venue;

            venue = await _venueCreator.Create(newVenue, venueOwnerAccount);
            _venueRepository.Save(venue);

            return venue;
        }

        public async Task<IEnumerable<IVenue>> SuggestVenues(IVenueSearchCriteria venueSearchCriteria)
        {
            var publicPlacesTask = _publicPlacesService.GetPublicPlaces(venueSearchCriteria);
            var venuesTask = _venueRepository.SearchVenues(venueSearchCriteria);

            await Task.WhenAll(publicPlacesTask, venuesTask);

            var placeSuggestions = _venueSuggestionsMerger.Merge(publicPlacesTask.Result, venuesTask.Result);

            return placeSuggestions;
        }

        public async Task<Venue> GetDefaultVenue(string publicPlaceId)
        {
            var venue = _venueRepository.GetVenue(publicPlaceId);
            if (venue != null) return venue;

            var publicPlace = await _publicPlacesService.GetPublicPlace(publicPlaceId);
            return _defaultVenueProvider.ProvideDefaultVenue(publicPlace);
        }
    }
}