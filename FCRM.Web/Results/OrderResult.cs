using FCRM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FCRM.Web.Results
{
    public class OrderResult : Result<OrderView>
    {
        public OrderResult(Order order) : base(new OrderView(order)) { }
        public OrderResult(string err) : base(err) { }
    }

    public class OrderListResult : ListResult<OrderView>
    {
        public OrderListResult(IEnumerable<Order> orders) : base(orders.Select(o=>new OrderView(o)).ToList()) { }

        public OrderListResult(string err) : base(err) { }
    }
}