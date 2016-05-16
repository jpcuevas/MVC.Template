using SiteZeras.Components.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace SiteZeras.Objects
{
    public class AccountResetView : BaseView
    {
        [Required]
        public String Token { get; set; }

        [Required]
        [NotTrimmed]
        public String NewPassword { get; set; }
    }
}
