namespace zavit.Web.Core.Context
{
    public interface IUserContextIocFactory
    {
        IUserContext Create();
        void Release(IUserContext userContext);
    }
}