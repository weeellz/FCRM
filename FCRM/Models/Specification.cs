using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FSRM.Models
{
    public class Task
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Guid { get; set; }
        [Required]
        public string TaskName { get; set; }
        [Required]
        public string TaskDescription{ get; set; }
        [Required]
        public double Price { get; set; }

        public bool Accepted { get; set; }
    }

    public class Specification
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Guid { get; set; }
        [Required]
        public User Preformer { get; set; }
        [Required]
        public List<Task> Tasks { get; set; }

        public Order Order { get; set; }
    }
}