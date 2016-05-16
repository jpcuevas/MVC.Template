using SiteZeras.Components.Mvc;
using SiteZeras.Objects;
using SiteZeras.Resources;
using NUnit.Framework;
using System;

namespace SiteZeras.Tests.Unit.Components.Mvc
{
    [TestFixture]
    public class DisplayNameMetadataProviderTests
    {
        #region Method: GetMetadataForProperty(Func<Object> modelAccessor, Type containerType, String propertyName)

        [Test]
        public void GetMetadataForProperty_SetsDisplayProperty()
        {
            DisplayNameMetadataProvider provider = new DisplayNameMetadataProvider();

            String actual = provider.GetMetadataForProperty(null, typeof(RoleView), "Name").DisplayName;
            String expected = ResourceProvider.GetPropertyTitle(typeof(RoleView), "Name");

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
