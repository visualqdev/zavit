using System.Collections.Generic;

namespace zavit.Domain.Activities
{
    public interface IActivityRepository
    {
        IEnumerable<Activity> GetDefaultActivities();
        IEnumerable<Activity> GetActivities(IEnumerable<int> activityIds);
        IEnumerable<Activity> GetAllActivities();
    }
}