using SiteZeras.Components.Alerts;
using SiteZeras.Controllers;
using SiteZeras.Services;
using SiteZeras.Validators;
using NSubstitute;
using NUnit.Framework;
using System.Web.Mvc;

namespace SiteZeras.Tests.Unit.Controllers
{
    [TestFixture]
    public class ValidatedControllerTests
    {
        private ValidatedController<IValidator, IService> controller;
        private IValidator validator;
        private IService service;

        [SetUp]
        public void SetUp()
        {
            service = Substitute.For<IService>();
            validator = Substitute.For<IValidator>();
            controller = Substitute.ForPartsOf<ValidatedController<IValidator, IService>>(validator, service);
        }

        #region Constructor: ValidatedController(TService service, TValidator validator)

        [Test]
        public void ValidatedController_SetsValidator()
        {
            IValidator actual = controller.Validator;
            IValidator expected = validator;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void ValidatedController_SetsValidatorAlerts()
        {
            AlertsContainer expected = controller.Alerts;
            AlertsContainer actual = validator.Alerts;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void ValidatedController_SetsModelState()
        {
            ModelStateDictionary expected = controller.ModelState;
            ModelStateDictionary actual = validator.ModelState;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: Dispose()

        [Test]
        public void Dispose_DisposesValidator()
        {
            controller.Dispose();

            validator.Received().Dispose();
        }

        [Test]
        public void Dispose_CanBeCalledMultipleTimes()
        {
            controller.Dispose();
            controller.Dispose();
        }

        #endregion
    }
}
