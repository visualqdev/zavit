namespace zavit.Domain.Clients
{
    public interface IClientRepository
    {
        Client FindClient(int clientId);
    }
}