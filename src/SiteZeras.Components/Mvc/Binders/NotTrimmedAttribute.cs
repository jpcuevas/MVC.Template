using System;

namespace SiteZeras.Components.Mvc
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class NotTrimmedAttribute : Attribute
    {
    }
}
