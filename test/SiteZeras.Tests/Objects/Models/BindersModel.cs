using SiteZeras.Components.Mvc;
using System;

namespace SiteZeras.Tests.Objects
{
    public class BindersModel
    {
        [NotTrimmed]
        public String NotTrimmed { get; set; }

        public String Trimmed { get ;set; }
    }
}
