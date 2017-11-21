using System.ComponentModel.DataAnnotations;

namespace FCRM.Web.Models
{
    public class Work : IdGuid
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        public double Price { get; set; }

        public bool Accepted { get; set; }

        [Required]
        public virtual Specification Specification { get; set; }
    }
}