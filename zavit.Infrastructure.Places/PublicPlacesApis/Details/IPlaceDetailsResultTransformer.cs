using zavit.Domain.Venues.PublicPlaces;

namespace zavit.Infrastructure.Places.PublicPlacesApis.Details
{
    public interface IPlaceDetailsResultTransformer
    {
        PublicPlace Transform(GooglePlaceDetailsResult googlePlacesDetailsResult);
    }
}