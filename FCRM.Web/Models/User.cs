using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FCRM.Web.Models
{
    public class UserView
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Id { get; set; }
        public IList<string> Roles { get; set; }

        public UserView(User user)
        {
            Roles = new List<string>();
            Name = user.Name;
            Surname = user.Surname;
            Id = user.Id;
            foreach (var element in user.Roles)
            {
                Roles.Add(element.Role.Name);
            }
        }
    }
    public class User : IdentityUser
    {
        public User()
            : base()
        {
            Specifications = new List<Specification>();
            Orders = new List<Order>();
            MyCompanies = new List<Company>();
        }

        [Required]
        [MaxLength(IdGuid.DEFAULT_LENGTH)]
        public string Name { get; set; }

        [MaxLength(IdGuid.DEFAULT_LENGTH)]
        public string Surname { get; set; }

        [Required]
        public virtual Company Company { get; set; }

        public virtual IList<Specification> Specifications { get; set; }

        public virtual IList<Order> Orders { get; set; }

        [InverseProperty("Owner")]
        public virtual IList<Company> MyCompanies { get; set; }

    }
}