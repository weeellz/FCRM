using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace FCRM.Web.Models
{
    public enum OrderStage
    {
        Started = 0,
        Сonversation = 1,
        Develop = 2,
        Complete = 3
    }
    public class OrderView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public string Email { get; set; }

        public string CompanyName { get; set; }

        public string Information { get; set; }

        public double? Price { get; set; }
        public string Manager { get; set; }
        public OrderStage Stage { get; set; }

        public List<string> Performers { get; set; }

        public OrderView(Order order)
        {
            Id = order.Id;
            Name = order.Name;
            Phone = order.Phone;
            Email = order.Email;
            CompanyName = order.CompanyName;
            Information = order.Information;
            Price = order.Price;
            Manager = order.Manager == null ? "unussigned" : order.Manager.Name;
            Stage = order.Stage;
            Performers = order.Specifications.Select(o => o.Preformer.Id).ToList();
        }
    }
    public class Order : IdGuid
    {
        public Order()
        {
            Specifications = new List<Specification>();
            Stage = OrderStage.Started;
            Price = 0;
        }

        [Required]
        [MaxLength(DEFAULT_LENGTH)]

        public string Name { get; set; }

        [Required]
        [MaxLength(DEFAULT_LENGTH)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(DEFAULT_LENGTH)]
        public string Email { get; set; }

        [Required]
        [MaxLength(DEFAULT_LENGTH)]
        public string CompanyName { get; set; }

        [Required]
        [MaxLength(MAX_LENGTH)]
        public string Information { get; set; }

        public double? Price { get; set; }
        public OrderStage Stage { get; set; }

        [Required]
        public virtual Company Company { get; set; }
        public virtual User Manager { get; set; }
        public virtual IList<Specification> Specifications { get; set; }

        

    }
}