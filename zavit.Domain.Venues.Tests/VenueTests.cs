using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mspec.Contrib;
using zavit.Domain.Activities;

namespace zavit.Domain.Venues.Tests 
{
    [Subject("Venue")]
    public class VenueTests : TestOf<Venue>
    {
        class When_adding_activities
        {
            Because of = () => Subject.AddActivities(_newActivities);

            It should_add_the_activities_that_are_part_of_the_venue_yet = () => Subject.Activities.ShouldContainOnly(_existingActivity, _activityAlreadyAdded, _newActivity);

            Establish context = () =>
            {
                _existingActivity = NewInstanceOf<Activity>();
                _existingActivity.Id = 2;

                _activityAlreadyAdded = NewInstanceOf<Activity>();
                _activityAlreadyAdded.Id = 3;

                Subject.Activities = new List<Activity> { _existingActivity, _activityAlreadyAdded };

                var duplicateActivity = NewInstanceOf<Activity>();
                duplicateActivity.Id = _activityAlreadyAdded.Id;

                _newActivity = NewInstanceOf<Activity>();
                _newActivity.Id = 1;

                _newActivities = new[] { _newActivity, duplicateActivity };
            };

            static IEnumerable<Activity> _newActivities;
            static Activity _existingActivity;
            static Activity _activityAlreadyAdded;
            static Activity _newActivity;
        }
    }
}

