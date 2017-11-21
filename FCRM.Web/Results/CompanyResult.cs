using FCRM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FCRM.Web.Results
{
    public class CompanyResult:Result<Company>
    {
        public CompanyResult(Company c) : base(c) { }
        public CompanyResult(string err) : base(err) { }
    }
}