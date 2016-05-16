﻿using SiteZeras.Components.Mvc;
using SiteZeras.Resources.Shared;
using SiteZeras.Tests.Objects;
using NUnit.Framework;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SiteZeras.Tests.Unit.Components.Mvc
{
    [TestFixture]
    public class StringLengthAdapterTests
    {
        #region Constructor: StringLengthAdapter(ModelMetadata metadata, ControllerContext context, StringLengthAttribute attribute)

        [Test]
        public void StringLengthAdapter_SetsMaxLengthErrorMessage()
        {
            ModelMetadata metadata = new DataAnnotationsModelMetadataProvider()
                .GetMetadataForProperty(null, typeof(AdaptersModel), "StringLength");
            StringLengthAttribute attribute = new StringLengthAttribute(128);
            new StringLengthAdapter(metadata, new ControllerContext(), attribute);

            String expected = Validations.FieldMustNotExceedLength;
            String actual = attribute.ErrorMessage;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void StringLengthAdapter_SetsMinMaxLengthErrorMessage()
        {
            StringLengthAttribute attribute = new StringLengthAttribute(128) { MinimumLength = 4 };
            ModelMetadata metadata = new DataAnnotationsModelMetadataProvider()
                .GetMetadataForProperty(null, typeof(AdaptersModel), "StringLength");
            new StringLengthAdapter(metadata, new ControllerContext(), attribute);

            String expected = Validations.FieldMustBeInRangeOfLength;
            String actual = attribute.ErrorMessage;

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
