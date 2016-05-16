using SiteZeras.Components.Alerts;
using SiteZeras.Data.Core;
using System;
using System.Web.Mvc;

namespace SiteZeras.Validators
{
    public abstract class BaseValidator : IValidator
    {
        private Boolean disposed;

        public ModelStateDictionary ModelState
        {
            get;
            set;
        }
        public AlertsContainer Alerts
        {
            get;
            set;
        }
        protected IUnitOfWork UnitOfWork
        {
            get;
            private set;
        }

        protected BaseValidator(IUnitOfWork unitOfWork)
        {
            Alerts = new AlertsContainer();
            UnitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(Boolean disposing)
        {
            if (disposed) return;

            UnitOfWork.Dispose();

            disposed = true;
        }
    }
}
