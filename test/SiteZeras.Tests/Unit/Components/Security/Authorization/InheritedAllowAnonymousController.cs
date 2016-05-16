﻿using System.Web.Mvc;

namespace SiteZeras.Tests.Unit.Components.Security
{
    public abstract class InheritedAllowAnonymousController : AllowAnonymousController
    {
        [HttpGet]
        public abstract ViewResult InheritanceGet();

        [HttpPost]
        public abstract ViewResult InheritancePost();

        [HttpGet]
        [ActionName("InheritanceGetName")]
        public abstract ViewResult InheritanceGetAction();

        [HttpPost]
        [ActionName("InheritancePostName")]
        public abstract ViewResult InheritancePostAction();
    }
}
