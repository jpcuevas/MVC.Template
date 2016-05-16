using SiteZeras.Components.Mail;
using NUnit.Framework;

namespace SiteZeras.Tests.Unit.Components.Mail
{
    [TestFixture]
    public class SmtpMailClientTests
    {
        private SmtpMailClient client;

        [SetUp]
        public void SetUp()
        {
            client = new SmtpMailClient();
        }

        #region Dispose()

        [Test]
        public void Dispose_CanBeCalledMultipleTimes()
        {
            client.Dispose();
            client.Dispose();
        }

        #endregion
    }
}
