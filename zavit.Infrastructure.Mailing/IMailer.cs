using System.Threading.Tasks;

namespace zavit.Infrastructure.Mailing
{
    public interface IMailer
    {
        Task SendMail(string subject, string htmlBody, string plainText, string recipient);
    }
}