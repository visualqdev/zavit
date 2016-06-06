using zavit.Domain.Places;
using zavit.Web.Api.Dtos.Places;

namespace zavit.Web.Api.DtoFactories.Places
{
    public interface IPlaceDtoFactory
    {
        PlaceDto CreateItem(IPlace place);
    }
}