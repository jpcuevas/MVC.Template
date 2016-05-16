using LightInject;
using SiteZeras.Components.Logging;
using SiteZeras.Components.Mail;
using SiteZeras.Components.Mvc;
using SiteZeras.Components.Security;
using SiteZeras.Controllers;
using SiteZeras.Data.Core;
using SiteZeras.Data.Logging;
using SiteZeras.Services;
using SiteZeras.Validators;
using System.Data.Entity;
using System.Web.Hosting;
using System.Web.Mvc;

namespace SiteZeras.Web.DependencyInjection
{
    public class MainContainer : ServiceContainer
    {
        public virtual void RegisterServices()
        {
            Register<DbContext, Context>();
            Register<IUnitOfWork, UnitOfWork>();

            Register<ILogger, Logger>();
            Register<IAuditLogger, AuditLogger>();

            Register<IHasher, BCrypter>();
            Register<IMailClient, SmtpMailClient>();

            Register<IRouteConfig, RouteConfig>();
            Register<IBundleConfig, BundleConfig>();
            Register<IExceptionFilter, ExceptionFilter>();

            Register<IMvcSiteMapParser, MvcSiteMapParser>();
            Register<IMvcSiteMapProvider>(factory => new MvcSiteMapProvider(
                 HostingEnvironment.MapPath("~/Mvc.sitemap"),
                 factory.GetInstance<IMvcSiteMapParser>()));

            Register<IGlobalizationProvider>(factory =>
                new GlobalizationProvider(HostingEnvironment.MapPath("~/Globalization.xml")));
            RegisterInstance<IAuthorizationProvider>(new AuthorizationProvider(typeof(BaseController).Assembly));

            Register<IRoleService, RoleService>();
            Register<IAccountService, AccountService>();

            Register<IRoleValidator, RoleValidator>();
            Register<IAccountValidator, AccountValidator>();
        }
    }
}
