using System;
using System.IO;

namespace zavit.Mailing.EmailTemplates
{
    public class MailerBase
    {
        protected string ViewBasePath => Path.Combine(AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory, "EmailTemplates");
    }
}