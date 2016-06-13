using zavit.Domain.Places.PublicPlaces;

namespace zavit.Infrastructure.Places.PublicPlacesApis
{
    public interface IPublicPlaceTransformer
    {
        PublicPlace Transform(GooglePlace googlePlace);
    }
}