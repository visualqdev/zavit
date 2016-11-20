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

        public IList<Activity> GetDefaultActivities()
        {
            return _session.QueryOver<Activity>()
                .Where(a => a.IsDefault)
                .OrderBy(a => a.Name).Asc
                .List();
        }

        public IList<Activity> GetActivities(IEnumerable<int> activityIds)
        {
            return _session.QueryOver<Activity>()
                .WhereRestrictionOn(a => a.Id).IsIn(activityIds.ToArray())
                .List();
        }

        public IEnumerable<Activity> GetAllActivities()
        {
            return _session.QueryOver<Activity>()
                .OrderBy(a => a.Name).Asc
                .List();
        }
    }
}