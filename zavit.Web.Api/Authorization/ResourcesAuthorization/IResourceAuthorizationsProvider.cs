using System.Collections.Generic;

namespace zavit.Web.Api.Authorization.ResourcesAuthorization
{
    public interface IResourceAuthorizationsProvider
    {
        IEnumerable<IResourceAuthorization> GetResourceAuthorizations();
        void Release(IResourceAuthorization accessAuthorization);
    }
}