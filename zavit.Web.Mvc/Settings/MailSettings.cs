using System.Configuration;
using zavit.Infrastructure.Mailing;

namespace zavit.Web.Mvc.Settings
{
    public class MailSettings : IMailSettings
    {
        string _key;
        public string Key => _key ?? (_key = ConfigurationManager.AppSettings["Mailer.Key"]);

        string _senderName;
        public string SenderName => _senderName ?? (_senderName = ConfigurationManager.AppSettings["Mailer.Sender.Name"]);

        string _senderEmail;
        public string SenderEmail => _senderEmail ?? (_senderEmail = ConfigurationManager.AppSettings["Mailer.Sender.Email"]);

        string _websiteUrl;
        public string WebsiteUrl => _websiteUrl ?? (_websiteUrl = ConfigurationManager.AppSettings["Mailer.WebsiteUrl"]);
    }
}