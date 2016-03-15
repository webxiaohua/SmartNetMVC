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
    }

    public class UserController
    {
        public string Login()
        {
            return "Success";
        }

        public object GetUser()
        {
            User user = new User { Name = "Robin", Age = 23 };
            return new PageResult("/FirstDemo.aspx", user);
        }

        public object GetSchool()
        {
            User user = new User { Name = "Robin", Age = 23 };
            School school = new School { Name = "清华大学" };
            return new PageResult("/SecondDemo.aspx", school);
        }
    }
}