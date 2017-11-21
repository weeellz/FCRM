using FSRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FSRM.Results
{
    public class OrderResult : Result<Order>
    {
        public OrderResult(Order order) : base(order) { }
        public OrderResult(string err) : base(err) { }
    }

    public class OrderListResult : ListResult<Order>
    {
        public OrderListResult(IEnumerable<Order> orders) : base(orders.ToList()) { }

        public OrderListResult(string err) : base(err) { }
    }
}