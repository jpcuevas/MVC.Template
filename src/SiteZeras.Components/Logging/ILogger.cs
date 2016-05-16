using System;

namespace SiteZeras.Components.Logging
{
    public interface ILogger : IDisposable
    {
        void Log(String message);
    }
}
