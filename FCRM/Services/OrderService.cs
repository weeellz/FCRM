using FSRM.Models;
using FSRM.Requests;
using FSRM.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FSRM.Services
{
    /// <summary>
    /// Order Service - класс, отвечающий за клиентские заказы. 
    /// Клиент добавляет заказ на сайте, менеджер забирает заказ под свой контроль,
    /// договаривается о цене заказа и назначает на него исполнителей
    /// 
    /// Все пользователи системы могут отслеживать статус заказа, но только менеджер может что-то менять
    /// </summary>
    public class OrderService
    {
        private SiteContext db = new SiteContext();

        private User authUser; //текущий пользователь

        public OrderListResult GetOrders()
        {
            var orders = db.Orders;
            return new OrderListResult(orders);
        }

        public OrderResult GetOrder(Guid guid)
        {
            var order = db.Orders.Find(guid);
            if(order!=null)
                return new OrderResult(order);
            return new OrderResult(string.Format("Заказа с id {0} не найдено", guid));
        }

        public void AddOrder(AddOrderRequest model)
        {
            var order = new Order();
            order.Name = model.Name;
            order.Email = model.Email;
            order.Company = model.Company;
            order.Information = model.Information;
            order.Phone = model.Phone;
            db.Orders.Add(order);
            db.SaveChanges();
        }

        public Result BecomeManager(Guid orderId)
        {
            var order = db.Orders.Find(orderId);
            if (order.Manager == null && authUser.isInRole("Manager"))
            {
                order.Manager = authUser;
                db.SaveChanges();
                return Result.Empty;
            }
            return new Result("Вы не можете стать менеджером этого проекта");
        }

        public Result ChangeManager(Guid orderId, User manager)
        {
            var order = db.Orders.Find(orderId);
            if (order.Manager == authUser && manager.isInRole("Manager"))
            {
                order.Manager = manager;
                db.SaveChanges();
                return Result.Empty;
            }
            return new Result("Вы не можете назначить этого пользователя менеджером этого проекта");
        }

        public Result AddPerformer(Guid orderId, User user)
        {
            var order = db.Orders.Find(orderId);
            if (order.Price == null)
            {
                return new Result("вы не можете назначать исполнителей, пока не определена цена проекта");
            }
            if (order.Manager == authUser && !order.Preformers.Contains(user))
            {
                order.Preformers.Add(user);
                db.SaveChanges();
                return Result.Empty;
            }
            return new Result("вы не можете назначить этого пользователя исполнителем проекта");
        }
    }
}