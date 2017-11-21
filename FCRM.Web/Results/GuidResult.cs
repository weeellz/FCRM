using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FCRM.Web.Results
{
    public class GuidResult : Result<Guid>
    {
        public GuidResult(Guid guid) : base(guid) { }
        public GuidResult(string err) : base(err) { }
    }
}