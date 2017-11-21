using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FSRM.Requests
{
    public class AddOrderRequest
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string Information { get; set; }
    }
}