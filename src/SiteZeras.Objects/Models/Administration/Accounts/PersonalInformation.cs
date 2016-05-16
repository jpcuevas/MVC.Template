using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteZeras.Objects
{
    public class PersonalInformation :BaseModel
    {

        [Required]
        [StringLength(128)]
        public String firstName { get; set; }

        [Required]
        [StringLength(128)]
        public String lastName { get; set; }

        public string AccountId { get; set; }

        public virtual Account Account { get; set; }
    }
}
