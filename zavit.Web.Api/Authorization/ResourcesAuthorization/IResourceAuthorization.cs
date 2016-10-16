namespace zavit.Web.Api.Authorization.ResourcesAuthorization
{
    public interface IResourceAuthorization
    {
        bool AuthorizeAccess(ResourceAuthorizeAttribute accessAttribute, IActionContext actionContext);
    }
}