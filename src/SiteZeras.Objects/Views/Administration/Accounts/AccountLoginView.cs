using SiteZeras.Components.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace SiteZeras.Objects
{
    public class AccountLoginView : BaseView
    {
        [Required]
        public String Username { get; set; }

        [Required]
        [NotTrimmed]
        public String Password { get; set; }
    }
}
