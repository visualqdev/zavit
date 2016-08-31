using zavit.Domain.Places.PublicPlaces;

namespace zavit.Domain.Places.Suggestions
{
    public class PublicPlaceSuggestionFactory : IPublicPlaceSuggestionFactory
    {
        public IPlace Create(PublicPlace publicPlace)
        {
            return new PublicPlaceSuggestion
            {
                PublicPlace = publicPlace
            };
        }
    }
}