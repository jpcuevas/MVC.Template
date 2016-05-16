using System.Collections.Generic;

namespace SiteZeras.Components.Mvc
{
    public interface IMvcSiteMapProvider
    {
        IEnumerable<MvcSiteMapNode> GetAuthorizedMenus();
        IEnumerable<MvcSiteMapNode> GetBreadcrumb();
    }
}
