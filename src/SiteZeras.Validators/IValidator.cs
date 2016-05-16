using SiteZeras.Components.Alerts;
using System;
using System.Web.Mvc;

namespace SiteZeras.Validators
{
    public interface IValidator : IDisposable
    {
        ModelStateDictionary ModelState { get; set; }
        AlertsContainer Alerts { get; set; }
    }
}
