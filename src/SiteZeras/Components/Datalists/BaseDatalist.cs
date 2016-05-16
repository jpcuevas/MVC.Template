﻿using Datalist;
using SiteZeras.Data.Core;
using SiteZeras.Objects;
using SiteZeras.Resources;
using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace SiteZeras.Components.Datalists
{
    public class BaseDatalist<TModel, TView> : GenericDatalist<TView>
        where TModel : BaseModel
        where TView : BaseView
    {
        protected IUnitOfWork UnitOfWork { get; set; }

        public BaseDatalist()
        {
            DialogTitle = ResourceProvider.GetDatalistTitle<TModel>();
            UrlHelper urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            DatalistUrl = urlHelper.Action(typeof(TModel).Name, Prefix, new { area = "" });
        }
        public BaseDatalist(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        protected override String GetColumnHeader(PropertyInfo property)
        {
            DatalistColumnAttribute column = property.GetCustomAttribute<DatalistColumnAttribute>(false);
            if (column != null && column.Relation != null)
                return GetColumnHeader(property.PropertyType.GetProperty(column.Relation));

            return ResourceProvider.GetPropertyTitle(typeof(TView), property.Name) ?? "";
        }
        protected override String GetColumnCssClass(PropertyInfo property)
        {
            Type type = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
            if (type.IsEnum) return "text-cell";

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.SByte:
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                    return "number-cell";
                case TypeCode.DateTime:
                    return "date-cell";
                default:
                    return "text-cell";
            }
        }

        protected override IQueryable<TView> GetModels()
        {
            return UnitOfWork.Select<TModel>().To<TView>();
        }
    }
}
