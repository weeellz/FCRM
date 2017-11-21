using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FCRM.Web.Models
{
    public class Specification : IdGuid
    {
        public Specification()
        {
            Tasks = new List<Work>();
        }

        public virtual User Preformer { get; set; }

        [Required]
        public virtual Order Order { get; set; }

        public virtual IList<Work> Tasks { get; set; }
    }
}