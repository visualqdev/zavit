using zavit.Domain.Activities;
using zavit.Web.Api.Dtos.Venues;

namespace zavit.Web.Api.DtoFactories.Venues
{
    public class VenueActivityDtoFactory : IVenueActivityDtoFactory
    {
        public VenueActivityDto CreateItem(Activity activity)
        {
            return new VenueActivityDto
            {
                Name = activity.Name,
                Id = activity.Id
            };
        }
    }
}