using System.Collections.Generic;
using zavit.Domain.Places.PublicPlaces;

namespace zavit.Infrastructure.Places.PublicPlacesApis
{
    public interface IPublicPlacesTransformer
    {
        IEnumerable<PublicPlace> Transform(GooglePlacesSearchResult googlePlacesSearchResult);
    }
}