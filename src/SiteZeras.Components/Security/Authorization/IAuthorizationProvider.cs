using System;

namespace SiteZeras.Components.Security
{
    public interface IAuthorizationProvider
    {
        Boolean IsAuthorizedFor(String accountId, String area, String controller, String action);

        void Refresh();
    }
}
