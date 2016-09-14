using zavit.Domain.Activities;
using zavit.Web.Api.Dtos.Venues;

namespace zavit.Web.Api.DtoFactories.Venues
{
    public interface IVenueActivityDtoFactory
    {
        VenueActivityDto CreateItem(Activity activity);
    }
}