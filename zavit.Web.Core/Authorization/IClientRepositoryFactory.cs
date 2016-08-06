using zavit.Domain.Clients;

namespace zavit.Web.Core.Authorization
{
    public interface IClientRepositoryFactory
    {
        IClientRepository Create();
        void Release(IClientRepository clientRepository);
    }
}