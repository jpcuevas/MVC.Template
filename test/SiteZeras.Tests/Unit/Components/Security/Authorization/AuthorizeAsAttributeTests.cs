using SiteZeras.Components.Security;
using NUnit.Framework;
using System;

namespace SiteZeras.Tests.Unit.Components.Security
{
    [TestFixture]
    public class AuthorizeAsAttributeTests
    {
        #region Constructor: AuthorizeAsAttribute(String action)

        [Test]
        public void AuthorizeAsAttribute_SetsAction()
        {
            String actual = new AuthorizeAsAttribute("Action").Action;
            String expected = "Action";

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
