using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FCRM.Web.Models
{
    public class Company : IdGuid
    {
        public Company()
        {
            Members = new List<User>();
        }

        [Required]
        [MaxLength(DEFAULT_LENGTH)]
        public string Name { get; set; }

        //[ForeignKey("OwnerId")]
        public virtual User Owner { get; set; }

        [InverseProperty("Company")]
        public virtual IList<User> Members { get; set; } 
    }
}