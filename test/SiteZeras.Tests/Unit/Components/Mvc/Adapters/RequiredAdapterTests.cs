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
    public class RequiredAdapterTests
    {
        #region Constructor: RequiredAdapter(ModelMetadata metadata, ControllerContext context, RequiredAttribute attribute)

        [Test]
        public void RequiredAdapter_SetsRequiredErrorMessage()
        {
            ModelMetadata metadata = new DataAnnotationsModelMetadataProvider()
                .GetMetadataForProperty(null, typeof(AdaptersModel), "Required");
            RequiredAttribute attribute = new RequiredAttribute();
            new RequiredAdapter(metadata, new ControllerContext(), attribute);

            String expected = Validations.FieldIsRequired;
            String actual = attribute.ErrorMessage;

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
