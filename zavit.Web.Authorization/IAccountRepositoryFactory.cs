using zavit.Domain.Accounts;

namespace zavit.Web.Authorization
{
    public interface IAccountRepositoryFactory
    {
        IAccountRepository Create();
        void Release(IAccountRepository accountRepository);
    }
}