using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using Rhino.Mspec.Contrib.Extensions;
using zavit.Domain.Activities;
using zavit.Web.Api.Controllers;
using zavit.Web.Api.DtoFactories.Venues;
using zavit.Web.Api.Dtos.Venues;

namespace zavit.Web.Api.Tests.Controllers 
{
    [Subject("ActivitiesController")]
    public class ActivitiesControllerTests : TestOf<ActivitiesController>
    {
        class When_getting_all_activities
        {
            Because of = () => _result = Subject.GetAll();

            It should_return_an_activity_dto_for_each_activity = () => _result.ShouldContainOnlyOrdered(_activityDto, _otherActivityDto);

            Establish context = () =>
            {
                var activity = NewInstanceOf<Activity>();
                var otherActivity = NewInstanceOf<Activity>();

                Injected<IActivityRepository>().Stub(r => r.GetAllActivities()).Return(new[] { activity, otherActivity });

                _activityDto = NewInstanceOf<VenueActivityDto>();
                _otherActivityDto = NewInstanceOf<VenueActivityDto>();

                Injected<IVenueActivityDtoFactory>().Stub(f => f.CreateItem(activity)).Return(_activityDto);
                Injected<IVenueActivityDtoFactory>().Stub(f => f.CreateItem(otherActivity)).Return(_otherActivityDto);
            };

            static IEnumerable<VenueActivityDto> _result;
            static VenueActivityDto _activityDto;
            static VenueActivityDto _otherActivityDto;
        }
    }
}

