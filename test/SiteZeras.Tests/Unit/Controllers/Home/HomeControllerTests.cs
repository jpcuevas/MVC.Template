﻿using SiteZeras.Controllers;
using SiteZeras.Services;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Web.Mvc;

namespace SiteZeras.Tests.Unit.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        private HomeController controller;
        private IAccountService service;

        [SetUp]
        public void SetUp()
        {
            service = Substitute.For<IAccountService>();
            controller = Substitute.ForPartsOf<HomeController>(service);

            controller.When(sub => { String get = sub.CurrentAccountId; }).DoNotCallBase();
            controller.CurrentAccountId.Returns("CurrentAccount");
        }

        #region Method: Index()

        [Test]
        public void Index_RedirectsToLogoutIfAccountDoesNotExist()
        {
            service.AccountExists(controller.CurrentAccountId).Returns(false);

            RedirectToRouteResult actual = controller.Index() as RedirectToRouteResult;

            Assert.AreEqual("Auth", actual.RouteValues["controller"]);
            Assert.AreEqual("Logout", actual.RouteValues["action"]);
            Assert.AreEqual(2, actual.RouteValues.Count);
        }

        [Test]
        public void Index_ReturnsEmptyView()
        {
            service.AccountExists(controller.CurrentAccountId).Returns(true);

            Object model = (controller.Index() as ViewResult).Model;

            Assert.IsNull(model);
        }

        #endregion

        #region Method: Error()

        [Test]
        public void Error_ReturnsEmptyView()
        {
            Object model = (controller.Error() as ViewResult).Model;

            Assert.IsNull(model);
        }

        #endregion

        #region Method: NotFound()

        [Test]
        public void NotFound_ReturnsEmptyView()
        {
            Object model = (controller.NotFound() as ViewResult).Model;

            Assert.IsNull(model);
        }

        #endregion

        #region Method: Unauthorized()

        [Test]
        public void Unauthorized_RedirectsToLogoutIfAccountDoesNotExist()
        {
            service.AccountExists(controller.CurrentAccountId).Returns(false);

            RedirectToRouteResult actual = controller.Unauthorized() as RedirectToRouteResult;

            Assert.AreEqual("Auth", actual.RouteValues["controller"]);
            Assert.AreEqual("Logout", actual.RouteValues["action"]);
            Assert.AreEqual(2, actual.RouteValues.Count);
        }

        [Test]
        public void Unauthorized_ReturnsEmptyView()
        {
            service.AccountExists(controller.CurrentAccountId).Returns(true);

            Object model = (controller.Unauthorized() as ViewResult).Model;

            Assert.IsNull(model);
        }

        #endregion
    }
}
