using System.Collections.Generic;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Type;
using zavit.Domain.Places.Search;
using zavit.Domain.Places.VenuePlaces;

namespace zavit.Infrastructure.Places.Repositories
{
    public class VenuePlaceRepository : IVenuePlaceRepository
    {
        readonly ISession _session;

        public VenuePlaceRepository(ISession session)
        {
            _session = session;
        }

        public void Save(VenuePlace place)
        {
            _session.Save(place);
            _session.Flush();
        }

        public void Update(VenuePlace place)
        {
            _session.Update(place);
            _session.Flush();
        }

        public VenuePlace Get(string placeId)
        {
            var venuePlace = _session.QueryOver<VenuePlace>()
                .Where(p => p.PlaceId == placeId)
                .SingleOrDefault();

            return venuePlace;
        }

        public Task<IEnumerable<VenuePlace>> SearchPlaces(IPlaceSearchCriteria placeSearchCriteria)
        {
            var queryOver = _session.QueryOver<VenuePlace>()
                .Where(NHibernate.Criterion.Expression.Sql("(6367 * acos(cos(radians(?)) * cos(radians({alias}.Latitude)) * cos(radians({alias}.Longitude) - radians(?)) + sin(radians(?)) * sin(radians({alias}.Latitude)))) < ?",
                    new object[] { placeSearchCriteria.Latitude.ToString(), placeSearchCriteria.Longitude.ToString(), placeSearchCriteria.Latitude.ToString(), placeSearchCriteria.Radius },
                    new IType[] { NHibernateUtil.Decimal, NHibernateUtil.Decimal, NHibernateUtil.Decimal, NHibernateUtil.Int32 }))
                .List();

            return Task.FromResult((IEnumerable<VenuePlace>)queryOver);
        }
    }
}