using System.Web.Routing;

namespace SiteZeras.Controllers
{
    public interface IRouteConfig
    {
        void RegisterRoutes(RouteCollection routes);
    }
}
