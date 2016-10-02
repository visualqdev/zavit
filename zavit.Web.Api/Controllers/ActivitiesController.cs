using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using zavit.Domain.Activities;
using zavit.Web.Api.DtoFactories.Venues;
using zavit.Web.Api.Dtos.Venues;

namespace zavit.Web.Api.Controllers
{
    public class ActivitiesController : ApiController
    {
        readonly IActivityRepository _activityRepository;
        readonly IVenueActivityDtoFactory _venueActivityDtoFactory;

        public ActivitiesController(IActivityRepository activityRepository, IVenueActivityDtoFactory venueActivityDtoFactory)
        {
            _activityRepository = activityRepository;
            _venueActivityDtoFactory = venueActivityDtoFactory;
        }

        [HttpGet]
        public IEnumerable<VenueActivityDto> GetAll()
        {
            var activities = _activityRepository.GetAllActivities();
            return activities.Select(a => _venueActivityDtoFactory.CreateItem(a));
        }
    }
}