using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Transform;
using NHibernate.Type;
using zavit.Domain.Venues;
using zavit.Domain.Venues.Search;
using zavit.Infrastructure.Venues.CustomOrdering;

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
                .Fetch(v => v.Activities).Eager;

            if (string.IsNullOrWhiteSpace(venueSearchCriteria.Name))
            {
                queryOver.Where(
                    NHibernate.Criterion.Expression.Sql(
                        "(6367 * acos(cos(radians(?)) * cos(radians({alias}.Latitude)) * cos(radians({alias}.Longitude) - radians(?)) + sin(radians(?)) * sin(radians({alias}.Latitude)))) < ?",
                        new object[]
                        {
                            venueSearchCriteria.Latitude.ToString(),
                            venueSearchCriteria.Longitude.ToString(),
                            venueSearchCriteria.Latitude.ToString(),
                            venueSearchCriteria.Radius/1000
                        },
                        new IType[]
                        {
                            NHibernateUtil.Decimal,
                            NHibernateUtil.Decimal,
                            NHibernateUtil.Decimal,
                            NHibernateUtil.Int32
                        }));
            }
            else
            {
                queryOver
                    .WhereRestrictionOn(v => v.Name).IsLike($"%{string.Join("%", venueSearchCriteria.Name.Split(' '))}%")
                    .UnderlyingCriteria.AddOrder(new CustomOrder($"geography::Point({venueSearchCriteria.Latitude}, {venueSearchCriteria.Longitude}, 4326).STDistance(geography::Point(Latitude, Longitude, 4326))"));
            }

            queryOver.TransformUsing(Transformers.DistinctRootEntity);

            var results = queryOver.List();

            return Task.FromResult((IEnumerable<Venue>)results);
        }

        public void Save(Venue venue)
        {
            _session.Save(venue);
            _session.Flush();
        }
    }
}