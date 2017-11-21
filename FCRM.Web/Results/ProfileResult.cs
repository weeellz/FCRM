using FCRM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FCRM.Web.Results
{
    public class ProfileResult:Result<User>
    {
        public ProfileResult(User user) : base(user) { }
        public ProfileResult(string err) : base(err) { }
    }
}