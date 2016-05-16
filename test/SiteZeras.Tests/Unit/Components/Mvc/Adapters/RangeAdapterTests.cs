﻿using SiteZeras.Components.Mvc;
using SiteZeras.Resources.Shared;
using SiteZeras.Tests.Objects;
using NUnit.Framework;
using System;
using System.Web.Mvc;
using DataAnnotations = System.ComponentModel.DataAnnotations;

namespace SiteZeras.Tests.Unit.Components.Mvc
{
    [TestFixture]
    public class RangeAdapterTests
    {
        #region Constructor: RangeAdapter(ModelMetadata metadata, ControllerContext context, RangeAttribute attribute)

        [Test]
        public void RangeAdapter_SetsRangeErrorMessage()
        {
            DataAnnotations.RangeAttribute attribute = new DataAnnotations.RangeAttribute(0, 128);
            ModelMetadata metadata = new DataAnnotationsModelMetadataProvider()
                .GetMetadataForProperty(null, typeof(AdaptersModel), "Range");
            new RangeAdapter(metadata, new ControllerContext(), attribute);

            String expected = Validations.FieldMustBeInRange;
            String actual = attribute.ErrorMessage;

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
