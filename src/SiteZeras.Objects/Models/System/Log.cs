﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace SiteZeras.Objects
{
    public class Log : BaseModel
    {
        [StringLength(128)]
        public String AccountId { get; set; }

        [Required]
        public String Message { get; set; }

        public Log()
        {
        }
        public Log(String message)
        {
            AccountId = HttpContext.Current.User.Identity.Name;
            Message = message;
        }
    }
}
