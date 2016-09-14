namespace zavit.Infrastructure.Core
{
    public interface IContainer
    {
        void Release(object instance);
        TDependency Resolve<TDependency>();
    }
}