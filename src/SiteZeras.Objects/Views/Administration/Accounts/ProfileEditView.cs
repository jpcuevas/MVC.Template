using SiteZeras.Components.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace SiteZeras.Objects
{
    public class ProfileEditView : BaseView
    {
        [Required]
        [StringLength(128)]
        public String Username { get; set; }

        [Required]
        [NotTrimmed]
        public String Password { get; set; }

        [NotTrimmed]
        public String NewPassword { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(256)]
        public String Email { get; set; }

        [Required]
        [StringLength(128)]
        public String firstName { get; set; }

        [Required]
        [StringLength(128)]
        public String lastName { get; set; }


    }
}
