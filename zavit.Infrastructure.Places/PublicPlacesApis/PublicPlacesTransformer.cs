using System.Collections.Generic;
using System.Linq;
using zavit.Domain.Places.PublicPlaces;

namespace zavit.Infrastructure.Places.PublicPlacesApis
{
    public class PublicPlacesTransformer : IPublicPlacesTransformer
    {
        readonly IPublicPlaceTransformer _publicPlaceTransformer;

        public PublicPlacesTransformer(IPublicPlaceTransformer publicPlaceTransformer)
        {
            _publicPlaceTransformer = publicPlaceTransformer;
        }

        public IEnumerable<PublicPlace> Transform(GooglePlacesSearchResult googlePlacesSearchResult)
        {
            var publicPlaces = googlePlacesSearchResult.results.Select(r => _publicPlaceTransformer.Transform(r));
            return publicPlaces;
        }
    }
}