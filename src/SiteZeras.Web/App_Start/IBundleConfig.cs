using System.Web.Optimization;

namespace SiteZeras.Web
{
    public interface IBundleConfig
    {
        void RegisterBundles(BundleCollection bundles);
    }
}
