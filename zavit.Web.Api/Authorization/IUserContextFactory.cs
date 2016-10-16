using zavit.Web.Core.Context;

namespace zavit.Web.Api.Authorization
{
    public interface IUserContextFactory
    {
        IUserContext Create();
        void Release(IUserContext userContext);
    }
}