using zavit.Domain.Accounts;

namespace zavit.Domain.VenueMemberships.NewVenueMembershipCreation
{
    public interface IVenueMembershipCreator
    {
        VenueMembership Create(Account account, NewVenueMembership newVenueMembership);
    }
}