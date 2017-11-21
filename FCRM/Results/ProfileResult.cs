using FSRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FSRM.Results
{
    public class ProfileResult:Result<User>
    {
        public ProfileResult(User user) : base(user) { }
        public ProfileResult(string err) : base(err) { }
    }
}