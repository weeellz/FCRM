using FCRM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FCRM.Web.Results
{
    public class TaskResult : Result<Work>
    {
        public TaskResult(Work work):base(work) { }
        public TaskResult(string err) : base(err) { } 
    }
}