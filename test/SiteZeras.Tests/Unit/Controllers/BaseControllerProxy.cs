using SiteZeras.Controllers;
using System;
using System.Web.Mvc;

namespace SiteZeras.Tests.Unit.Controllers
{
    public class BaseControllerProxy : BaseController
    {
        public RedirectToRouteResult BaseRedirectToAction(String action)
        {
            return RedirectToAction(action);
        }

        public IAsyncResult BaseBeginExecuteCore(AsyncCallback callback, Object state)
        {
            return BeginExecuteCore(callback, state);
        }
        public void BaseOnAuthorization(AuthorizationContext filterContext)
        {
            OnAuthorization(filterContext);
        }
        public void BaseOnActionExecuted(ActionExecutedContext context)
        {
            OnActionExecuted(context);
        }
    }
}
