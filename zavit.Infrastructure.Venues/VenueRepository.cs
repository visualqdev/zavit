using NHibernate;
using zavit.Domain.Venues;

namespace zavit.Infrastructure.Venues
{
    public class VenueRepository : IVenueRepository
    {
        readonly ISession _session;

        public VenueRepository(ISession session)
        {
            _session = session;
        }

        public Venue GetVenue(int id)
        {
            return _session.Get<Venue>(id);
        }
    }
}