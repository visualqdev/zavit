using zavit.Domain.Venues.PublicPlaces;

namespace zavit.Domain.Venues.Search
{
    public interface IPublicVenueSuggestionFactory
    {
        IVenue Create(PublicPlace publicPlace);
    }
}