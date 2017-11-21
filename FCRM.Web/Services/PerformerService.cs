using FCRM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using FCRM.Web.Results;
using System.Security.Principal;
using System.Web.Mvc;

namespace FCRM.Web.Services
{
    public class PerformerService : Service
    {
        public SpecificationResult GetSpecification(Guid id)
        {
            using (var db = new SiteContext())
            {
                var spec = db.Specifications.Find(id);
                if (spec == null)
                {
                    return new SpecificationResult("Задания не существует");
                }
                if (spec.Preformer.Id != CurrentUser(db).Id)
                {
                    return new SpecificationResult("Это не ваше задание");
                }
                return new SpecificationResult(spec);
            }
        }

        public OrderResult GetOrder(Guid id)
        {
            using (var db = new SiteContext())
            {
                var order = db.Orders.Find(id);
                if (!CurrentUser(db).Orders.Any(x => x.Id == order.Id))
                {
                    return new OrderResult("Вы не имеете доступа к этому заказу");
                }
                return new OrderResult(order);
            }
        }

    }
}