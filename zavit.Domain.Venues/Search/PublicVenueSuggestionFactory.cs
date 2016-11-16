using zavit.Domain.Venues.PublicPlaces;

namespace zavit.Domain.Venues.Search
{
    public class PublicVenueSuggestionFactory : IPublicVenueSuggestionFactory
    {
        public IVenue Create(PublicPlace publicPlace)
        {
            return new PublicVenueSuggestion
            {
                PublicPlace = publicPlace
            };
        }
    }
}