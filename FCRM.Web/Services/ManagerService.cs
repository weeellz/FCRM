using FCRM.Web.Models;
using FCRM.Web.Results;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;

namespace FCRM.Web.Services
{
    public class ManagerService : Service
    {
        public Result AddPerformer(Guid orderId, Guid userId)
        {
            using (var db = new SiteContext())
            {
                var user = CurrentUser(db);

                var order = db.Orders.Find(orderId);

                if (order == null || order.Company.Id != user.Company.Id)
                {
                    return new Result("Проекта с таким идентификатором не найдено");
                }
                if (order.Manager.Id != user.Id)
                {
                    return new Result("Вы не являетесь менеджером проекта");
                }
                var performer = db.Users.Find(userId);
                if (performer == null)
                {
                    return new Result("Пользователь не найден");
                }

                if (performer.Company.Id != order.Company.Id)
                {
                    return new Result("Пользователь не относится к вашей фирме");
                }

                return CallUserManager<Result>(db, userManager =>
                {
                    if (!userManager.IsInRole(performer.Id, "performer"))
                    {
                        return new Result("Пользователь не является performer");
                    }
                    if (db.Specifications.Any(s => s.Preformer.Id == performer.Id && s.Order.Id == order.Id))
                    {
                        return new Result("Пользователь уже выполняет задание по этому заказу");
                    }
                    var spec = new Specification
                    {
                        Preformer = performer,
                        Order = order
                    };
                    db.Specifications.Add(spec);
                    db.SaveChanges();

                    return Result.Empty;
                });

            }
        }

        public Result SetOrderPrice(double Price, Guid orderId)
        {
            using (var db = new SiteContext())
            {
                var user = CurrentUser(db);

                var order = db.Orders.Find(orderId);

                if (order == null || order.Company.Id != user.Company.Id)
                {
                    return new Result("Проекта с таким идентификатором не найдено");
                }
                if (order.Manager.Id != user.Id)
                {
                    return new Result("Вы не являетесь менеджером проекта");
                }
                order.Price = Price;
                db.Entry<Order>(order).State = EntityState.Modified;
                db.SaveChanges();
                return Result.Empty;
            }
        }
    }
}