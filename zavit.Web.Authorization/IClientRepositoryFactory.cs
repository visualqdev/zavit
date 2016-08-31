using zavit.Domain.Clients;

namespace zavit.Web.Authorization
{
    public interface IClientRepositoryFactory
    {
        IClientRepository Create();
        void Release(IClientRepository clientRepository);
    }
}