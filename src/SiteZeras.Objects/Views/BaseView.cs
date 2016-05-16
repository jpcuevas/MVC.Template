﻿using System;
using System.ComponentModel.DataAnnotations;

namespace SiteZeras.Objects
{
    public abstract class BaseView
    {
        private String id;

        [Key]
        [Required]
        public String Id
        {
            get
            {
                return id ?? (id = Guid.NewGuid().ToString());
            }
            set
            {
                id = value;
            }
        }

        public DateTime? CreationDate
        {
            get;
            protected set;
        }

        protected BaseView()
        {
            DateTime now = DateTime.Now;
            CreationDate = new DateTime(now.Ticks / 100000 * 100000, now.Kind);
        }
    }
}
