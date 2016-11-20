using System;
using zavit.Domain.Accounts;

namespace zavit.Web.Core.Context
{
    public interface IUserContext
    {
        Account Account { get; }

        bool IsAuthenticated { get; }
    }
}