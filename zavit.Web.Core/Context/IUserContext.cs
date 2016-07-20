using zavit.Domain.Accounts;

namespace zavit.Web.Core.Context
{
    public interface IUserContext
    {
        Account Account { get; }
    }
}