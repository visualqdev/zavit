using System.Net;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using zavit.Domain.Shared;

namespace zavit.Infrastructure.Mailing
{
    public class Mailer : IMailer
    {
        readonly IMailSettings _mailSettings;
        readonly ILogger _logger;
        readonly SendGridClient _client;

        public Mailer(IMailSettings mailSettings, ILogger logger)
        {
            _mailSettings = mailSettings;
            _logger = logger;
            _client = new SendGridClient(_mailSettings.Key);
        }

        public async Task SendMail(string subject, string htmlBody, string plainText, string recipient)
        {
            var message = new SendGridMessage()
            {
                From = new EmailAddress(_mailSettings.SenderEmail, _mailSettings.SenderName),
                Subject = subject,
                PlainTextContent = plainText,
                HtmlContent = htmlBody
            };
            message.AddTo(new EmailAddress(recipient));
            var response = await _client.SendEmailAsync(message);
            if (response.StatusCode != HttpStatusCode.Accepted)
            {
                var responseBody = await response.Body.ReadAsStringAsync();
                _logger.Warn($"Failed to send individual email '{subject}' to '{recipient}', Status code: '{response.StatusCode}', Message from SendGrid: '{responseBody}'");
            }
        }
    }
}
