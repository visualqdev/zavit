using zavit.Domain.Places.PublicPlaces;

namespace zavit.Domain.Places.Suggestions
{
    public interface IPublicPlaceSuggestionFactory
    {
        IPlace Create(PublicPlace publicPlace);
    }
}