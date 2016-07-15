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
    }
}