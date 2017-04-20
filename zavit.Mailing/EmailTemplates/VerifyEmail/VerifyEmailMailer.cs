using System.IO;
using System.Threading.Tasks;
using RazorEngine;
using RazorEngine.Templating;
using zavit.Domain.Accounts;
using zavit.Domain.Accounts.Registrations;
using zavit.Infrastructure.Mailing;

namespace zavit.Mailing.EmailTemplates.VerifyEmail
{
    public class VerifyEmailMailer : MailerBase, IVerifyEmailMailer
    {
        readonly IMailer _mailer;
        readonly IMailSettings _mailSettings;

        public VerifyEmailMailer(IMailer mailer, IMailSettings mailSettings)
        {
            _mailer = mailer;
            _mailSettings = mailSettings;
        }

        public async Task SendMail(Account account)
        {
            if (!account.NeedsVerification) return;

            var model = new VerifyEmailModel
            {
                RecipientDisplayName = account.Profile.DisplayName,
                VerifyAccountUrl = $"{_mailSettings.WebsiteUrl}/accountverification/{account.VerificationCode}"
            };

            var path = Path.Combine(ViewBasePath, "VerifyEmail", "VerifyEmailTemplate.cshtml");
            var template = File.ReadAllText(path);
            var body = Engine.Razor.RunCompile(template, "verifyEmailTemplate", typeof(VerifyEmailModel), model);
            var plainText = PlainTextBody(model);

            await _mailer.SendMail("Verify email", body, plainText, "richard.vidis@live.co.uk");
        }

        static string PlainTextBody(VerifyEmailModel verifyEmailModel)
        {
            return $"Thank you for registering with ZAVIT. To make sure you can easily arrange your activities please verify your email address by clicking the following link {verifyEmailModel.VerifyAccountUrl}";
        }
    }
}
