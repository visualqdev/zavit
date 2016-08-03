namespace zavit.Web.Api.Authorization
{
    public interface IClaimsIdentityProviderFactory
    {
        IClaimsIdentityProvider Create();
        void Release(IClaimsIdentityProvider claimsIdentityProvider);
    }
}