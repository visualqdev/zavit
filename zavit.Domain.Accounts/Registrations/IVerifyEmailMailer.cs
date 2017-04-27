using System.Threading.Tasks;

namespace zavit.Domain.Accounts.Registrations
{
    public interface IVerifyEmailMailer
    {
        Task SendMail(Account account);
    }
}