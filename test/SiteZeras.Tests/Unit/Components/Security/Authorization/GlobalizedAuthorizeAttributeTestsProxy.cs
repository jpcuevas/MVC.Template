using SiteZeras.Components.Security;
using System.Web.Mvc;

namespace SiteZeras.Tests.Unit.Components.Security
{
    public class GlobalizedAuthorizeAttributeProxy : GlobalizedAuthorizeAttribute
    {
        public void BaseHandleUnauthorizedRequest(AuthorizationContext context)
        {
            HandleUnauthorizedRequest(context);
        }
    }
}
