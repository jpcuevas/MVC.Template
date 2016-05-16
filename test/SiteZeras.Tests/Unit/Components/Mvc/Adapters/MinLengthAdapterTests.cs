using SiteZeras.Components.Mvc;
using SiteZeras.Resources.Shared;
using SiteZeras.Tests.Objects;
using NUnit.Framework;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SiteZeras.Tests.Unit.Components.Mvc
{
    [TestFixture]
    public class MinLengthAdapterTests
    {
        #region Constructor: MinLengthAdapter(ModelMetadata metadata, ControllerContext context, MinLengthAttribute attribute)

        [Test]
        public void MinLengthAdapter_SetsMinLengthErrorMessage()
        {
            ModelMetadata metadata = new DataAnnotationsModelMetadataProvider()
                .GetMetadataForProperty(null, typeof(AdaptersModel), "MinLength");

            MinLengthAttribute attribute = new MinLengthAttribute(128);
            new MinLengthAdapter(metadata, new ControllerContext(), attribute);

            String expected = Validations.FieldMustBeWithMinLengthOf;
            String actual = attribute.ErrorMessage;

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
