using System.Collections.Generic;
using System.Threading.Tasks;
using NHibernate;
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
            var query = "SELECT Id, Latitude, Longitude,(6367 * acos(cos(radians(:center_lat)) * cos(radians(Latitude)) * cos(radians(Longitude) - radians(:center_lng)) + sin(radians(:center_lat)) * sin(radians(Latitude)))) AS distance FROM VenuePlace WHERE (6367 * acos(cos(radians(:center_lat)) * cos(radians(Latitude)) * cos(radians(Longitude) - radians(:center_lng)) + sin(radians(:center_lat)) * sin(radians(Latitude)))) < :radius ORDER BY (6367 * acos(cos(radians(:center_lat)) * cos(radians(Latitude)) * cos(radians(Longitude) - radians(:center_lng)) + sin(radians(:center_lat)) * sin(radians(Latitude)))) ASC";
            var result = _session.CreateQuery(query)
                .SetParameter("center_lat", placeSearchCriteria.Latitude)
                .SetParameter("center_lng", placeSearchCriteria.Longitude)
                .SetParameter("radius", placeSearchCriteria.Radius)
                .List();

            return Task.FromResult((IEnumerable<VenuePlace>)new List<VenuePlace>());
        }
    }
}