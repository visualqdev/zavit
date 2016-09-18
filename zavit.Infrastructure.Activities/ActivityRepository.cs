using System.Collections.Generic;
using System.Linq;
using NHibernate;
using zavit.Domain.Activities;

namespace zavit.Infrastructure.Activities
{
    public class ActivityRepository : IActivityRepository
    {
        readonly ISession _session;

        public ActivityRepository(ISession session)
        {
            _session = session;
        }

        public IEnumerable<Activity> GetDefaultActivities()
        {
            return _session.QueryOver<Activity>()
                .List();
        }

        public IEnumerable<Activity> GetActivities(IEnumerable<int> activityIds)
        {
            return _session.QueryOver<Activity>()
                .WhereRestrictionOn(a => a.Id).IsIn(activityIds.ToArray())
                .List();
        }
    }
}