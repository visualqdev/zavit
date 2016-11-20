namespace zavit.Web.Api.Authorization.ClaimsIdentities
{
    public interface IClaimsIdentityProviderFactory
    {
        IClaimsIdentityProvider Create();
        void Release(IClaimsIdentityProvider claimsIdentityProvider);
    }
}