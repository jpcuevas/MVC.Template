using System.Collections.Generic;
using System.Xml.Linq;

namespace SiteZeras.Components.Mvc
{
    public interface IMvcSiteMapParser
    {
        IEnumerable<MvcSiteMapNode> GetNodeTree(XElement siteMap);
    }
}
