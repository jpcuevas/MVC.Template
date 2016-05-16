using System;
using System.ComponentModel.DataAnnotations;

namespace SiteZeras.Objects
{
    public class AccountEditView : BaseView
    {
        [Editable(false)]
        public String Username { get; set; }

        public String RoleId { get; set; }
        public String RoleName { get; set; }
    }
}
