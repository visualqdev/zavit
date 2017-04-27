using System.Net;

namespace zavit.Infrastructure.Mailing
{
    public interface IMailSettings
    {
        string Key { get; }
        string SenderName { get; }
        string SenderEmail { get; }
        string WebsiteUrl { get; }
    }
}