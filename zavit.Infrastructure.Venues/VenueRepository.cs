using System.Collections.Generic;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Type;
using zavit.Domain.Venues;
using zavit.Domain.Venues.Search;

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

        public Venue GetVenue(string publicPlaceId)
        {
            return _session.QueryOver<Venue>()
                .Where(v => v.PublicPlaceId == publicPlaceId)
                .SingleOrDefault();
        }

        public Task<IEnumerable<Venue>> SearchVenues(IVenueSearchCriteria venueSearchCriteria)
        {
            var queryOver = _session.QueryOver<Venue>()
                .Where(NHibernate.Criterion.Expression.Sql("(6367 * acos(cos(radians(?)) * cos(radians({alias}.Latitude)) * cos(radians({alias}.Longitude) - radians(?)) + sin(radians(?)) * sin(radians({alias}.Latitude)))) < ?",
                    new object[] { venueSearchCriteria.Latitude.ToString(), venueSearchCriteria.Longitude.ToString(), venueSearchCriteria.Latitude.ToString(), venueSearchCriteria.Radius / 1000 },
                    new IType[] { NHibernateUtil.Decimal, NHibernateUtil.Decimal, NHibernateUtil.Decimal, NHibernateUtil.Int32 }))
                .List();

            return Task.FromResult((IEnumerable<Venue>)queryOver);
        }

        public void Save(Venue venue)
        {
            _session.Save(venue);
            _session.Flush();
        }
    }
}