using System.Threading.Tasks;
using zavit.Domain.Accounts;

namespace zavit.Domain.Venues.NewVenueCreation
{
    public interface IVenueCreator
    {
        Task<Venue> Create(NewVenue newVenue, Account venueOwnerAccount);
    }
}