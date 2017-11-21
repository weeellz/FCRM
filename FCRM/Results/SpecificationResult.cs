using FSRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FSRM.Results
{
    public class SpecificationResult:Result<Specification>
    {
        public SpecificationResult(Specification task) : base(task){}
        public SpecificationResult(string err) : base(err) { }
    }
}