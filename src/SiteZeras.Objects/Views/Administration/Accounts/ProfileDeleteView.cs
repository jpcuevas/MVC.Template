using SiteZeras.Components.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace SiteZeras.Objects
{
    public class ProfileDeleteView : BaseView
    {
        [Required]
        [NotTrimmed]
        public String Password { get; set; }
    }
}
