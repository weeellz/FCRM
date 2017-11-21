using FCRM.Web.Models;
using FCRM.Web.Requests;
using FCRM.Web.Results;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FCRM.Web.Services
{
    public class CompanyService : Service
    {
        public OrderListResult GetOrders()
        {
            using (var db = new SiteContext())
            {
                var companyId = CurrentCompanyId(db);
                var orders = db.Orders.Where(o => o.Company.Id == companyId).ToList();
                return new OrderListResult(orders);
            }
        }
        public CompanyResult GetCompanyInfo()
        {
            using (var db = new SiteContext())
            {
                var companyId = CurrentCompanyId(db);
                var company = db.Companies.Find(companyId);
                if (company == null)
                {
                    return new CompanyResult("Компания не найдена");
                }
                return new CompanyResult(company);
            }
        }

        public Result AddOrder(Guid companyId, OrderRequest model)
        {
            try
            {
                string err = string.Join<string>(" ", model.Validate());
                if (!string.IsNullOrEmpty(err))
                    return new Result(err);
                using (var db = new SiteContext())
                {
                    var comp = db.Companies.Find(companyId);
                    if (comp == null)
                    {
                        return new Result("Такой комании не существует");
                    }
                    var order = new Order
                    {
                        Name = model.Name,
                        Phone = model.Phone,
                        Information = model.Information,
                        CompanyName = model.CompanyName,
                        Email = model.Email,
                        Company = comp
                    };
                    db.Orders.Add(order);
                    db.SaveChanges();
                    return Result.Empty;
                }
            }
            catch
            {
                return new Result("Не удалось добавить заказ");
            }
        }

        public UserListResult GetUsers()
        {
            using (var db = new SiteContext())
            {
                var companyId = CurrentCompanyId(db);
                return new UserListResult(db.Users.Where(u => u.Company.Id == companyId).ToList());
            }
        }
        public Result AddRole(string UserId, string RoleName)
        {
            if (!new string[] { "manager", "owner", "performer" }.Contains(RoleName))
            {
                return new Result("Такой роли не существует");
            }
            using (var db = new SiteContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Id == UserId);
                if (user == null)
                {
                    return new Result("Пользователь не найден");
                }
                if (CurrentCompanyId(db) != user.Company.Id)
                {
                    return new Result("Вы не имеете право изменять роли пользователей вне своей фирмы");
                }

                return CallUserManager<Result>(db, userManager =>
                {
                    if (userManager.IsInRole(UserId, RoleName))
                    {
                        return new Result("Пользователь уже имеет эту роль");
                    }

                    userManager.AddToRole(UserId, RoleName);

                    return Result.Empty;
                });
            }
        }
        public Result RemoveRole(string UserId, string RoleName)
        {
            if (!new string[] { "manager", "owner", "performer" }.Contains(RoleName))
            {
                return new Result("Такой роли не существует");
            }
            using (var db = new SiteContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Id == UserId);
                if (user == null)
                {
                    return new Result("Пользователь не найден");
                }
                if (CurrentCompanyId(db) != user.Company.Id)
                {
                    return new Result("Вы не имеете право изменять роли пользователей вне своей фирмы");
                }

                return CallUserManager<Result>(db, userManager =>
                {
                    if (!userManager.IsInRole(UserId, RoleName))
                    {
                        return new Result("Пользователь и так не имеет этой роли");
                    }

                    userManager.RemoveFromRole(UserId, RoleName);

                    return Result.Empty;
                });
            }
        }
        public GuidResult GetCompanyGuid()
        {
            using (var db = new SiteContext())
            {
                return new GuidResult(CurrentCompanyId(db));
            }
        }
        public Result AssignToOrder(Guid orderId)
        {
            using (var db = new SiteContext())
            {
                var order = db.Orders.Find(orderId);
                var cuser = CurrentUser(db);
                if (cuser.Company.Id != order.Company.Id)
                {
                    return new Result("Вы не имеете доступа к этому заказу");
                }
                if (order.Manager != null)
                {
                    return new Result("на этот проект уже назначен менеджер");
                }
                order.Manager = cuser;
                order.Stage = OrderStage.Сonversation;
                db.Entry<Order>(order).State = EntityState.Modified;
                db.SaveChanges();
                return Result.Empty;
            }
        }
        public OrderListResult GetUnassignedOrders()
        {
            using (var db = new SiteContext())
            {
                var companyId = CurrentCompanyId(db);
                var orders = db.Orders.Where(o => o.Company.Id == companyId && o.Stage == OrderStage.Started).ToList();
                return new OrderListResult(orders);
            }
        }
        public OrderListResult GetMyOrders()
        {
            using (var db = new SiteContext())
            {
                var user = CurrentUser(db);
                var companyId = user.Company.Id;
                var orders = db.Orders.Where(o => o.Company.Id == companyId && o.Manager.Id == user.Id).ToList();
                return new OrderListResult(orders);
            }
        }
        public UserListResult GetPerformersList()
        {
            using (var db = new SiteContext())
            {
                var user = CurrentUser(db);
                var companyId = user.Company.Id;
                var performers = db.Users.Where(u => u.Company.Id == companyId && u.Roles.Any(r => r.Role.Name == "performer")).ToList();
                return new UserListResult(performers);
            }
        }
        public Result<int> GetPerformersCount()
        {
            using (var db = new SiteContext())
            {
                var user = CurrentUser(db);
                var companyId = user.Company.Id;
                var performersCount = db.Users.Where(u => u.Company.Id == companyId && u.Roles.Any(r => r.Role.Name == "performer")).Count();
                return new Result<int>(performersCount);
            }
        }
        public Result<int> GetManagersCount()
        {
            using (var db = new SiteContext())
            {
                var user = CurrentUser(db);
                var companyId = user.Company.Id;
                var managersCount = db.Users.Where(u => u.Company.Id == companyId && u.Roles.Any(r => r.Role.Name == "manager")).Count();
                return new Result<int>(managersCount);
            }
        }
        public StringResult GetCompanyName()
        {
            using (var db = new SiteContext())
            {
                var user = CurrentUser(db);
                var companyName = user.Company.Name;
                return new StringResult(companyName);
            }
        }
    }
}