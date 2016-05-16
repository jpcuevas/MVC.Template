﻿using SiteZeras.Objects;
using NSubstitute;
using NUnit.Framework;
using System;

namespace SiteZeras.Tests.Unit.Objects
{
    [TestFixture]
    public class BaseViewTests
    {
        private BaseView view;

        [SetUp]
        public void SetUp()
        {
            view = Substitute.For<BaseView>();
        }

        #region Constructor: BaseView()

        [Test]
        public void BaseView_SetsCreationDateToNow()
        {
            Int64 actual = Substitute.For<BaseView>().CreationDate.Value.Ticks;
            Int64 expected = DateTime.Now.Ticks;

            Assert.AreEqual(expected, actual, 10000000);
        }

        [Test]
        public void BaseView_TruncatesMicrosecondsFromCreationDate()
        {
            DateTime actual = Substitute.For<BaseView>().CreationDate.Value;
            DateTime expected = new DateTime(actual.Year, actual.Month, actual.Day, actual.Hour, actual.Minute, actual.Second, actual.Millisecond);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void BaseView_KeepsCurrentDateKind()
        {
            DateTimeKind actual = Substitute.For<BaseView>().CreationDate.Value.Kind;
            DateTimeKind expected = DateTime.Now.Kind;

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Property: Id

        [Test]
        public void Id_AlwaysGetsNotNull()
        {
            view.Id = null;

            Assert.IsNotNull(view.Id);
        }

        [Test]
        public void Id_AlwaysGetsUniqueValue()
        {
            String expected = view.Id;
            view.Id = null;
            String actual = view.Id;

            Assert.AreNotEqual(expected, actual);
        }

        [Test]
        public void Id_AlwaysGetsSameValue()
        {
            String expected = view.Id;
            String actual = view.Id;

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
