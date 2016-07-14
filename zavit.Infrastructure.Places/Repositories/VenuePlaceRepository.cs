using NHibernate;
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
            
        }

        public VenuePlace Get(string placeId)
        {
            var venuePlace = _session.QueryOver<VenuePlace>()
                .Where(p => p.PlaceId == placeId)
                .SingleOrDefault();

            return venuePlace;
        }
    }
}