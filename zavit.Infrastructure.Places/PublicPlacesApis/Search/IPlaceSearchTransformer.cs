using zavit.Domain.Places.PublicPlaces;

namespace zavit.Infrastructure.Places.PublicPlacesApis.Search
{
    public interface IPlaceSearchTransformer
    {
        PublicPlace Transform(GooglePlaceSearch googlePlace);
    }
}