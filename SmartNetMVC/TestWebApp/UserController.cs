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

        public object GetSchool(string name, DateTime time)
        {

            //User user = new User { Name = name, Age = age };
            School school = new School { Name = name, Time = time };
            return new PageResult("/SecondDemo.aspx", school);
        }
    }
}