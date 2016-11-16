using zavit.Domain.Venues.PublicPlaces;

namespace zavit.Infrastructure.Places.PublicPlacesApis.Search
{
    public interface IPlaceSearchTransformer
    {
        PublicPlace Transform(GooglePlaceSearch googlePlace);
    }
}