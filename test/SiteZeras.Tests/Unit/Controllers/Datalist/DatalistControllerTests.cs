﻿using Datalist;
using SiteZeras.Components.Datalists;
using SiteZeras.Components.Mvc;
using SiteZeras.Controllers;
using SiteZeras.Data.Core;
using SiteZeras.Objects;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace SiteZeras.Tests.Unit.Controllers
{
    [TestFixture]
    public class DatalistControllerTests
    {
        private DatalistController controller;
        private AbstractDatalist datalist;
        private IUnitOfWork unitOfWork;
        private DatalistFilter filter;

        [SetUp]
        public void SetUp()
        {
            unitOfWork = Substitute.For<IUnitOfWork>();
            controller = Substitute.ForPartsOf<DatalistController>(unitOfWork);

            HttpContext.Current = HttpContextFactory.CreateHttpContext();
            datalist = Substitute.For<AbstractDatalist>();
            filter = new DatalistFilter();
        }

        [TearDown]
        public void TearDown()
        {
            GlobalizationManager.Provider = null;
            HttpContext.Current = null;
        }

        #region Method: GetData(AbstractDatalist datalist, DatalistFilter filter, Dictionary<String, Object> additionalFilters = null)

        [Test]
        public void GetData_SetsDatalistCurrentFilter()
        {
            controller.GetData(datalist, filter);

            DatalistFilter actual = datalist.CurrentFilter;
            DatalistFilter expected = filter;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetData_SetsEmptyAdditionalFilters()
        {
            controller.GetData(datalist, filter);

            Int32 actual = filter.AdditionalFilters.Count;
            Int32 expected = 0;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetData_SetsAdditionalFilters()
        {
            Dictionary<String, Object> filters = new Dictionary<String, Object> { { "Key", "Value" } };
            controller.GetData(datalist, filter, filters);

            Dictionary<String, Object> actual = filter.AdditionalFilters;
            Dictionary<String, Object> expected = filters;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetData_ReturnsPublicJsonData()
        {
            datalist.GetData().Returns(new DatalistData());

            JsonResult actual = controller.GetData(datalist, filter);
            DatalistData expectedData = datalist.GetData();

            Assert.AreEqual(JsonRequestBehavior.AllowGet, actual.JsonRequestBehavior);
            Assert.AreSame(expectedData, actual.Data);
        }

        #endregion

        #region Method: Role(DatalistFilter filter)

        [Test]
        public void Role_GetsRolesData()
        {
            controller.When(sub => sub.GetData(Arg.Any<BaseDatalist<Role, RoleView>>(), filter, null)).DoNotCallBase();
            controller.GetData(Arg.Any<BaseDatalist<Role, RoleView>>(), filter, null).Returns(new JsonResult());
            GlobalizationManager.Provider = GlobalizationProviderFactory.CreateProvider();

            JsonResult expected = controller.GetData(null, filter, null);
            JsonResult actual = controller.Role(filter);

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Method: Dispose()

        [Test]
        public void Dispose_DisposesUnitOfWork()
        {
            controller.Dispose();

            unitOfWork.Received().Dispose();
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
