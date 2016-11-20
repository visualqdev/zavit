using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Activities;
using zavit.Domain.Venues;

namespace zavit.Domain.VenueMemberships.Tests 
{
    [Subject("VenueMembership")]
    public class VenueMembershipTests : TestOf<VenueMembership>
    {
        class When_updating_activities_that_have_some_new_activity_compared_to_the_current_activities
        {
            Because of = () => _result = Subject.UpdateActivities(_activities);

            It should_set_the_activities_to_be_the_new_activities =
                () => Subject.Activities.ShouldContainOnly(_activities);

            It should_add_the_new_activities_to_the_venue =
                () => Subject.Venue.AssertWasCalled(v => v.AddActivities(_activities));

            It should_return_true_to_indicate_that_activities_were_updated = () => _result.ShouldBeTrue();

            Establish context = () =>
            {
                Subject.Venue = NewInstanceOf<Venue>();

                var existingActivity = NewInstanceOf<Activity>();
                existingActivity.Id = 1;

                Subject.Activities = new List<Activity> { existingActivity };

                var newActivity = NewInstanceOf<Activity>();
                newActivity.Id = 2;

                _activities = new List<Activity> { newActivity };
            };

            static bool _result;
            static IList<Activity> _activities;
        }

        class When_updating_activities_that_are_missing_activity_compared_to_the_current_activities
        {
            Because of = () => _result = Subject.UpdateActivities(_activities);

            It should_set_the_activities_to_be_the_new_activities =
                () => Subject.Activities.ShouldContainOnly(_activities);

            It should_add_the_new_activities_to_the_venue =
                () => Subject.Venue.AssertWasCalled(v => v.AddActivities(_activities));

            It should_return_true_to_indicate_that_activities_were_updated = () => _result.ShouldBeTrue();

            Establish context = () =>
            {
                Subject.Venue = NewInstanceOf<Venue>();

                var existingActivity = NewInstanceOf<Activity>();
                existingActivity.Id = 1;

                var additionalExistingActivity = NewInstanceOf<Activity>();
                additionalExistingActivity.Id = 2;

                Subject.Activities = new List<Activity> { existingActivity, additionalExistingActivity };

                var newActivity = NewInstanceOf<Activity>();
                newActivity.Id = existingActivity.Id;

                _activities = new List<Activity> { newActivity };
            };

            static bool _result;
            static IList<Activity> _activities;
        }

        class When_updating_activities_that_are_same_as_the_current_activities
        {
            Because of = () => _result = Subject.UpdateActivities(_activities);

            It should_not_try_to_change_the_current_activities =
                () => Subject.Activities.ShouldContainOnly(_existingActivity);

            It should_add_any_activities_to_the_venue =
                () => Subject.Venue.AssertWasNotCalled(v => v.AddActivities(Arg<IEnumerable<Activity>>.Is.Anything));

            It should_return_false_to_indicate_that_no_activities_were_updated = () => _result.ShouldBeFalse();

            Establish context = () =>
            {
                Subject.Venue = NewInstanceOf<Venue>();

                _existingActivity = NewInstanceOf<Activity>();
                _existingActivity.Id = 1;

                Subject.Activities = new List<Activity> { _existingActivity };

                var newActivity = NewInstanceOf<Activity>();
                newActivity.Id = _existingActivity.Id;

                _activities = new List<Activity> { newActivity };
            };

            static bool _result;
            static IList<Activity> _activities;
            static Activity _existingActivity;
        }
    }
}

