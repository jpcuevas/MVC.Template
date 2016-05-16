using System;
using System.ComponentModel.DataAnnotations;

namespace SiteZeras.Objects
{
    public class AccountRecoveryView : BaseView
    {
        [Required]
        [EmailAddress]
        public String Email { get; set; }
    }
}
