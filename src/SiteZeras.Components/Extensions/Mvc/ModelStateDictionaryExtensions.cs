﻿using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace SiteZeras.Components.Extensions.Mvc
{
    public static class ModelStateDictionaryExtensions
    {
        public static void AddModelError<TModel>(this ModelStateDictionary modelState, Expression<Func<TModel, Object>> expression, Exception exception)
        {
            modelState.AddModelError(ExpressionHelper.GetExpressionText(expression), exception);
        }

        public static void AddModelError<TModel>(this ModelStateDictionary modelState, Expression<Func<TModel, Object>> expression, String errorMessage)
        {
            modelState.AddModelError(ExpressionHelper.GetExpressionText(expression), errorMessage);
        }
        public static void AddModelError<TModel>(this ModelStateDictionary modelState, Expression<Func<TModel, Object>> expression, String format, params Object[] args)
        {
            modelState.AddModelError(ExpressionHelper.GetExpressionText(expression), String.Format(format, args));
        }
    }
}
