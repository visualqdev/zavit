using zavit.Domain.Accounts;

namespace zavit.Web.Core.Context
{
    public class UserContext : IUserContext
    {
        public Account Account { get; set; }
    }
}