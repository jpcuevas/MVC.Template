﻿using GridMvc.Columns;
using GridMvc.Html;
using SiteZeras.Components.Extensions.Html;
using SiteZeras.Components.Security;
using SiteZeras.Resources;
using SiteZeras.Tests.Objects;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace SiteZeras.Tests.Unit.Components.Extensions.Html
{
    [TestFixture]
    public class GridMvcExtensionsTests
    {
        private IGridColumnCollection<AllTypesView> columns;
        private IGridHtmlOptions<AllTypesView> options;
        private IGridColumn<AllTypesView> column;
        private UrlHelper urlHelper;

        [SetUp]
        public void SetUp()
        {
            column = SubstituteColumn<AllTypesView>();
            options = SubstituteOptions<AllTypesView>();
            columns = SubstituteColumns<AllTypesView, DateTime?>(column);

            HttpContext.Current = HttpContextFactory.CreateHttpContext();
            urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);

            Authorization.Provider = Substitute.For<IAuthorizationProvider>();
            Authorization.Provider.IsAuthorizedFor(Arg.Any<String>(), Arg.Any<String>(), Arg.Any<String>(), Arg.Any<String>()).Returns(true);
        }

        [TearDown]
        public void TearDown()
        {
            Authorization.Provider = null;
            HttpContext.Current = null;
        }

        #region Extension method: AddActionLink<T>(this IGridColumnCollection<T> column, String action, String iconClass)

        [Test]
        public void AddActionLink_OnUnauthorizedActionLinkReturnsNull()
        {
            Authorization.Provider.IsAuthorizedFor(Arg.Any<String>(), Arg.Any<String>(), Arg.Any<String>(), Arg.Any<String>()).Returns(false);

            Assert.IsNull(columns.AddActionLink("Edit", "fa fa-pencil"));
        }

        [Test]
        public void AddActionLink_AddsGridColumn()
        {
            columns.AddActionLink("Edit", "fa fa-pencil");

            columns.Received().Add();
        }

        [Test]
        public void AddActionLink_DoesNotEncodeGridColumn()
        {
            columns.AddActionLink("Edit", "fa fa-pencil");

            column.Received().Encoded(false);
        }

        [Test]
        public void AddActionLink_DoesNotSanitizeGridColumn()
        {
            columns.AddActionLink("Edit", "fa fa-pencil");

            column.Received().Sanitized(false);
        }

        [Test]
        public void AddActionLink_SetsCssOnGridColumn()
        {
            columns.AddActionLink("Edit", "fa fa-pencil");

            column.Received().Css("action-cell");
        }

        [Test]
        public void AddActionLink_RendersAuthorizedActionLink()
        {
            Authorization.Provider.IsAuthorizedFor(Arg.Any<String>(), Arg.Any<String>(), Arg.Any<String>(), Arg.Any<String>()).Returns(false);
            Authorization.Provider.IsAuthorizedFor(Arg.Any<String>(), Arg.Any<String>(), Arg.Any<String>(), "Details").Returns(true);
            AllTypesView view = new AllTypesView();
            Authorization.Provider = null;
            String actionLink = "";

            column
                .RenderValueAs(Arg.Any<Func<AllTypesView, String>>()).Returns(column)
                .AndDoes(info => { actionLink = info.Arg<Func<AllTypesView, String>>().Invoke(view); });

            columns.AddActionLink("Details", "fa fa-info");

            String actual = actionLink;
            String expected = String.Format(
                "<a class=\"details-action\" href=\"{0}\">" +
                    "<i class=\"fa fa-info\"></i>" +
                "</a>",
                urlHelper.Action("Details", new { id = view.Id }));

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddActionLink_OnNullAuthorizationProviderRendersActionLink()
        {
            AllTypesView view = new AllTypesView();
            Authorization.Provider = null;
            String actionLink = "";

            column
                .RenderValueAs(Arg.Any<Func<AllTypesView, String>>()).Returns(column)
                .AndDoes(info => { actionLink = info.Arg<Func<AllTypesView, String>>().Invoke(view); });

            columns.AddActionLink("Details", "fa fa-info");

            String actual = actionLink;
            String expected = String.Format(
                "<a class=\"details-action\" href=\"{0}\">" +
                    "<i class=\"fa fa-info\"></i>" +
                "</a>",
                urlHelper.Action("Details", new { id = view.Id }));

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddActionLink_OnModelWihoutKeyPropertyThrows()
        {
            Func<Object, String> renderer = null;
            IGridColumn<Object> objectColumn = SubstituteColumn<Object>();
            IGridColumnCollection<Object> objectColumns = SubstituteColumns<Object, String>(objectColumn);

            objectColumn
                .RenderValueAs(Arg.Any<Func<Object, String>>())
                .Returns(objectColumn)
                .AndDoes(info =>
                {
                    renderer = info.Arg<Func<Object, String>>();
                });

            objectColumns.AddActionLink("Delete", "fa fa-times");

            String actual = Assert.Throws<Exception>(() => renderer.Invoke(new Object())).Message;
            String expected = "Object type does not have a key property.";

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Extension method: AddDateProperty<T>(this IGridColumnCollection<T> column, Expression<Func<T, DateTime?>> property)

        [Test]
        public void AddDateProperty_AddsGridColumn()
        {
            Expression<Func<AllTypesView, DateTime?>> propertyFunc = (model) => model.CreationDate;

            columns.AddDateProperty(propertyFunc);

            columns.Received().Add(propertyFunc);
        }

        [Test]
        public void AddDateProperty_SetsGridColumnTitle()
        {
            String expected = ResourceProvider.GetPropertyTitle<AllTypesView, DateTime?>(model => model.CreationDate);

            columns.AddDateProperty(model => model.CreationDate);

            column.Received().Titled(expected);
        }

        [Test]
        public void AddDateProperty_SetsGridColumnCss()
        {
            columns.AddDateProperty(model => model.CreationDate);

            column.Received().Css("date-cell");
        }

        [Test]
        public void AddDateProperty_FormatsGridColumn()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("lt-LT");
            String expected = String.Format("{{0:{0}}}", CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern);

            columns.AddDateProperty(model => model.CreationDate);

            column.Received().Format(expected);
        }

        #endregion

        #region Extension method: AddDateTimeProperty<T>(this IGridColumnCollection<T> column, Expression<Func<T, DateTime?>> property)

        [Test]
        public void AddDateTimeProperty_AddsGridColumn()
        {
            Expression<Func<AllTypesView, DateTime?>> propertyFunc = (model) => model.CreationDate;

            columns.AddDateTimeProperty(propertyFunc);

            columns.Received().Add(propertyFunc);
        }

        [Test]
        public void AddDateTimeProperty_SetsGridColumnTitle()
        {
            String expected = ResourceProvider.GetPropertyTitle<AllTypesView, DateTime?>(model => model.CreationDate);

            columns.AddDateTimeProperty(model => model.CreationDate);

            column.Received().Titled(expected);
        }

        [Test]
        public void AddDateTimeProperty_SetsGridColumnCss()
        {
            columns.AddDateTimeProperty(model => model.CreationDate);

            column.Received().Css("date-cell");
        }

        [Test]
        public void AddDateTimeProperty_FormatsGridColumn()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("lt-LT");
            String expected = String.Format("{{0:{0} {1}}}",
                CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern,
                CultureInfo.CurrentUICulture.DateTimeFormat.ShortTimePattern);

            columns.AddDateTimeProperty(model => model.CreationDate);

            column.Received().Format(expected);
        }

        #endregion

        #region Extension method: AddProperty<T, TProperty>(this IGridColumnCollection<T> column, Expression<Func<T, TProperty>> property)

        [Test]
        public void AddProperty_AddsGridColumn()
        {
            Expression<Func<AllTypesView, String>> propertyFunc = (model) => model.Id;

            columns.AddProperty(propertyFunc);

            columns.Received().Add(propertyFunc);
        }

        [Test]
        public void AddProperty_SetsGridColumnTitle()
        {
            String expected = ResourceProvider.GetPropertyTitle<AllTypesView, DateTime?>(model => model.CreationDate);

            columns.AddProperty(model => model.CreationDate);

            column.Received().Titled(expected);
        }

        [Test]
        public void AddProperty_SetsCssClassAsTextCellForEnum()
        {
            AssertCssClassFor(model => model.EnumField, "text-cell");
        }

        [Test]
        public void AddProperty_SetsCssClassAsNumberCellForSByte()
        {
            AssertCssClassFor(model => model.SByteField, "number-cell");
        }

        [Test]
        public void AddProperty_SetsCssClassAsNumberCellForByte()
        {
            AssertCssClassFor(model => model.ByteField, "number-cell");
        }

        [Test]
        public void AddProperty_SetsCssClassAsNumberCellForInt16()
        {
            AssertCssClassFor(model => model.Int16Field, "number-cell");
        }

        [Test]
        public void AddProperty_SetsCssClassAsNumberCellForUInt16()
        {
            AssertCssClassFor(model => model.UInt16Field, "number-cell");
        }

        [Test]
        public void AddProperty_SetsCssClassAsNumberCellForInt32()
        {
            AssertCssClassFor(model => model.Int32Field, "number-cell");
        }

        [Test]
        public void AddProperty_SetsCssClassAsNumberCellForUInt32()
        {
            AssertCssClassFor(model => model.UInt32Field, "number-cell");
        }

        [Test]
        public void AddProperty_SetsCssClassAsNumberCellForInt64()
        {
            AssertCssClassFor(model => model.Int64Field, "number-cell");
        }

        [Test]
        public void AddProperty_SetsCssClassAsNumberCellForUInt64()
        {
            AssertCssClassFor(model => model.UInt64Field, "number-cell");
        }

        [Test]
        public void AddProperty_SetsCssClassAsNumberCellForSingle()
        {
            AssertCssClassFor(model => model.SingleField, "number-cell");
        }

        [Test]
        public void AddProperty_SetsCssClassAsNumberCellForDouble()
        {
            AssertCssClassFor(model => model.DoubleField, "number-cell");
        }

        [Test]
        public void AddProperty_SetsCssClassAsNumberCellForDecimal()
        {
            AssertCssClassFor(model => model.DecimalField, "number-cell");
        }

        [Test]
        public void AddProperty_SetsCssClassAsDateCellForDateTime()
        {
            AssertCssClassFor(model => model.DateTimeField, "date-cell");
        }

        [Test]
        public void AddProperty_SetsCssClassAsTextCellForNullableEnum()
        {
            AssertCssClassFor(model => model.NullableEnumField, "text-cell");
        }

        [Test]
        public void AddProperty_SetsCssClassAsNumberCellForNullableSByte()
        {
            AssertCssClassFor(model => model.NullableSByteField, "number-cell");
        }

        [Test]
        public void AddProperty_SetsCssClassAsNumberCellForNullableByte()
        {
            AssertCssClassFor(model => model.NullableByteField, "number-cell");
        }

        [Test]
        public void AddProperty_SetsCssClassAsNumberCellForNullableInt16()
        {
            AssertCssClassFor(model => model.NullableInt16Field, "number-cell");
        }

        [Test]
        public void AddProperty_SetsCssClassAsNumberCellForNullableUInt16()
        {
            AssertCssClassFor(model => model.NullableUInt16Field, "number-cell");
        }

        [Test]
        public void AddProperty_SetsCssClassAsNumberCellForNullableInt32()
        {
            AssertCssClassFor(model => model.NullableInt32Field, "number-cell");
        }

        [Test]
        public void AddProperty_SetsCssClassAsNumberCellForNullableUInt32()
        {
            AssertCssClassFor(model => model.NullableUInt32Field, "number-cell");
        }

        [Test]
        public void AddProperty_SetsCssClassAsNumberCellForNullableInt64()
        {
            AssertCssClassFor(model => model.NullableInt64Field, "number-cell");
        }

        [Test]
        public void AddProperty_SetsCssClassAsNumberCellForNullableUInt64()
        {
            AssertCssClassFor(model => model.NullableUInt64Field, "number-cell");
        }

        [Test]
        public void AddProperty_SetsCssClassAsNumberCellForNullableSingle()
        {
            AssertCssClassFor(model => model.NullableSingleField, "number-cell");
        }

        [Test]
        public void AddProperty_SetsCssClassAsNumberCellForNullableDouble()
        {
            AssertCssClassFor(model => model.NullableDoubleField, "number-cell");
        }

        [Test]
        public void AddProperty_SetsCssClassAsNumberCellForNullableDecimal()
        {
            AssertCssClassFor(model => model.NullableDecimalField, "number-cell");
        }

        [Test]
        public void AddProperty_SetsCssClassAsDateCellForNullableDateTime()
        {
            AssertCssClassFor(model => model.NullableDateTimeField, "date-cell");
        }

        [Test]
        public void AddProperty_SetsCssClassAsTextCellForOtherTypes()
        {
            AssertCssClassFor(model => model.StringField, "text-cell");
        }

        #endregion

        #region Extension method: ApplyDefaults<T>(this IGridHtmlOptions<T> options)

        [Test]
        public void ApplyDefaults_SetsEmptyText()
        {
            options.ApplyDefaults();

            options.Received().EmptyText(SiteZeras.Resources.Table.Resources.NoDataFound);
        }

        [Test]
        public void ApplyDefaults_SetsLanguage()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("lt-LT");

            options.ApplyDefaults();

            options.Received().SetLanguage(CultureInfo.CurrentUICulture.Name);
        }

        [Test]
        public void ApplyDefaults_SetsName()
        {
            options.ApplyDefaults();

            options.Received().Named(typeof(AllTypesView).Name);
        }

        [Test]
        public void ApplyDefaults_EnablesMultipleFilters()
        {
            options.ApplyDefaults();

            options.Received().WithMultipleFilters();
        }

        [Test]
        public void ApplyDefaults_DisablesRowSelection()
        {
            options.ApplyDefaults();

            options.Received().Selectable(false);
        }

        [Test]
        public void ApplyDefaults_SetsPaging()
        {
            options.ApplyDefaults();

            options.Received().WithPaging(20);
        }

        [Test]
        public void ApplyDefaults_EnablesFiltering()
        {
            options.ApplyDefaults();

            options.Received().Filterable();
        }

        [Test]
        public void ApplyDefaults_EnablesSorting()
        {
            options.ApplyDefaults();

            options.Received().Sortable();
        }

        #endregion

        #region Test helpers

        private IGridColumn<TModel> SubstituteColumn<TModel>()
        {
            IGridColumn<TModel> column = Substitute.For<IGridColumn<TModel>>();
            column.RenderValueAs(Arg.Any<Func<TModel, String>>()).Returns(column);
            column.Sanitized(Arg.Any<Boolean>()).Returns(column);
            column.Encoded(Arg.Any<Boolean>()).Returns(column);
            column.Format(Arg.Any<String>()).Returns(column);
            column.Titled(Arg.Any<String>()).Returns(column);
            column.Css(Arg.Any<String>()).Returns(column);

            return column;
        }
        private IGridHtmlOptions<TModel> SubstituteOptions<TModel>()
        {
            IGridHtmlOptions<TModel> options = Substitute.For<IGridHtmlOptions<TModel>>();
            options.SetLanguage(Arg.Any<String>()).Returns(options);
            options.WithPaging(Arg.Any<Int32>()).Returns(options);
            options.EmptyText(Arg.Any<String>()).Returns(options);
            options.Named(Arg.Any<String>()).Returns(options);
            options.WithMultipleFilters().Returns(options);
            options.Selectable(false).Returns(options);
            options.Filterable().Returns(options);
            options.Sortable().Returns(options);

            return options;
        }
        private IGridColumnCollection<TModel> SubstituteColumns<TModel, TProperty>(IGridColumn<TModel> gridColumn)
        {
            IGridColumnCollection<TModel> columns = Substitute.For<IGridColumnCollection<TModel>>();
            columns.Add(Arg.Any<Expression<Func<TModel, TProperty>>>()).Returns(gridColumn);
            columns.Add().Returns(gridColumn);

            return columns;
        }

        private void AssertCssClassFor<TProperty>(Expression<Func<AllTypesView, TProperty>> property, String expected)
        {
            columns = SubstituteColumns<AllTypesView, TProperty>(column);
            columns.AddProperty(property);

            column.Received().Css(expected);
        }

        #endregion
    }
}
