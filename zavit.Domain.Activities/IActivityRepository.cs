using System.Collections.Generic;

namespace zavit.Domain.Activities
{
    public interface IActivityRepository
    {
        IList<Activity> GetDefaultActivities();
        IList<Activity> GetActivities(IEnumerable<int> activityIds);
        IEnumerable<Activity> GetAllActivities();
    }
}