using zavit.Domain.Accounts;

namespace zavit.Web.Core.Authorization
{
    public interface IAccountRepositoryFactory
    {
        IAccountRepository Create();
        void Release(IAccountRepository accountRepository);
    }
}