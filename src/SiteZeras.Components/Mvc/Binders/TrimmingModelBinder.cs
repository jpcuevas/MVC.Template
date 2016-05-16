﻿using System;
using System.Reflection;
using System.Web.Mvc;

namespace SiteZeras.Components.Mvc
{
    public class TrimmingModelBinder : IModelBinder
    {
        public Object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (value == null || value.AttemptedValue == null)
               return null;

            Type containerType = bindingContext.ModelMetadata.ContainerType;
            if (containerType != null)
            {
                PropertyInfo property = containerType.GetProperty(bindingContext.ModelName);
                if (property.GetCustomAttribute<NotTrimmedAttribute>() != null)
                    return value.AttemptedValue;
            }

            return value.AttemptedValue.Trim();
        }
    }
}
