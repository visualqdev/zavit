using System.Collections.Generic;

namespace zavit.Domain.Activities
{
    public interface IActivityRepository
    {
        IEnumerable<Activity> GetDefaultActivities();
    }
}