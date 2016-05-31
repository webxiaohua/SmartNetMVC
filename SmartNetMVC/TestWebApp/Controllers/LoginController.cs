using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Smart.NetMVC2;
using TestWebApp.Models;

namespace TestWebApp.Controllers
{
    public class LoginController : BaseController
    {
        public PageResult IndexAction(string loginName, string loginPwd)
        {
            if (String.IsNullOrEmpty(loginName) || String.IsNullOrEmpty(loginPwd))
            {
                return new PageResult("~/Views/Login.aspx", null);
            }
            else
            {
                LoginModel model = new LoginModel() { LoginName = loginName.ToString(), LoginPwd = loginPwd.ToString() };
                this.HttpContext.Session["CurrentUser"] = model;
                return new PageResult("~/Views/Login.aspx", model);
            }
        }

    }
}