﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace SiteZeras.Objects
{
    public class AuditLog : BaseModel
    {
        [StringLength(128)]
        public String AccountId { get; set; }

        [Required]
        [StringLength(128)]
        public String Action { get; set; }

        [Required]
        [StringLength(128)]
        public String EntityName { get; set; }

        [Required]
        [StringLength(128)]
        public String EntityId { get; set; }

        [Required]
        public String Changes { get; set; }

        public AuditLog()
        {
        }
        public AuditLog(String action, String entityName, String entityId, String changes)
        {
            AccountId = HttpContext.Current.User.Identity.Name;
            EntityName = entityName;
            EntityId = entityId;
            Changes = changes;
            Action = action;
        }
    }
}
