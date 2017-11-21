using FCRM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FCRM.Web.Results
{
    public class UserResult:Result<UserView>
    {
        public UserResult(User user) : base(new UserView(user)) { }
        public UserResult(string error) : base(error) { }
    }
    public class UserListResult : ListResult<UserView>
    {
        public UserListResult(List<User> users) : base(users.Select(u=>new UserView(u)).ToList()) { }
        public UserListResult(string err) : base(err) { }
    }
}