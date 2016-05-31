using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Smart.NetMVC2;

namespace TestWebApp
{
    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class School
    {
        public string Name { get; set; }
        public DateTime Time { get; set; }
    }

    public class LoginsController : BaseController
    {
        //[Action(Verb="POST")]
        public string LoginAction()
        {
            this.HttpContext.Session["SmartMVC_Current_UserRole"] = new string[] { "admin", "employee" };
            this.HttpContext.Session["SmartMVC_Current_UserIdentity"] = "admin";
            return "Success";
        }
    }

    [AllowRole(RoleList = new string[] { "admin", "manager" })]
    public class UserController
    {
        //[AllowRole(RoleList = new string[] { "admin", "manager" })]
        public object GetUserAction()
        {
            User user = new User { Name = "Robin", Age = 23 };
            return new PageResult("/FirstDemo.aspx", user);
        }
        [AllowUser(UserList = new string[] { "root" })]
        public object GetSchoolAction(string name, DateTime time)
        {

            //User user = new User { Name = name, Age = age };
            School school = new School { Name = name, Time = time };
            return new PageResult("/SecondDemo.aspx", school);
        }
    }



}