using System;

namespace SiteZeras.Components.Mail
{
    public interface IMailClient : IDisposable
    {
        void Send(String email, String subject, String body);
    }
}
