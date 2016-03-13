using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWebApp
{
    public class UserController
    {
        public string Login(string name)
        {
            return "Success" + name;
        }
    }
}