﻿using SiteZeras.Components.Mvc;
using SiteZeras.Objects;
using SiteZeras.Resources.Shared;
using NUnit.Framework;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SiteZeras.Tests.Unit.Components.Mvc
{
    [TestFixture]
    public class NumberValidatorTests
    {
        private NumberValidator validator;
        private ModelMetadata metadata;

        [SetUp]
        public void SetUp()
        {
            metadata = new DisplayNameMetadataProvider().GetMetadataForProperty(null, typeof(AccountView), "Username");
            validator = new NumberValidator(metadata, new ControllerContext());
        }

        #region Method: Validate(Object container)

        [Test]
        public void Validate_DoesNotValidate()
        {
            CollectionAssert.IsEmpty(validator.Validate(null));
        }

        #endregion

        #region Method: GetClientValidationRules()

        [Test]
        public void GetClientValidationRules_ReturnsDateValidationRule()
        {
            ModelClientValidationRule actual = validator.GetClientValidationRules().Single();
            ModelClientValidationRule expected = new ModelClientValidationRule
            {
                ValidationType = "number",
                ErrorMessage = String.Format(Validations.FieldMustBeNumeric, metadata.GetDisplayName())
            };

            Assert.AreEqual(expected.ValidationType, actual.ValidationType);
            Assert.AreEqual(expected.ErrorMessage, actual.ErrorMessage);
            CollectionAssert.IsEmpty(actual.ValidationParameters);
        }

        #endregion
    }
}
