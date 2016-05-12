using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;

namespace Smart.NetMVC2
{
    /// <summary>
    /// @Author:robin
    /// @Desc:mvc 路由控制
    /// </summary>
    public class URLRoute
    {
        /// <summary>
        /// 注册所有coltroller 和 action
        /// </summary>
        public static void RegisterRoutes()
        {
            foreach (var item in InitEngine.s_ControllerActionDict)
            {
                
            }

            RouteTable.Routes.MapRoute("User", "User/Login", defaults: new { controller = "User", action = "Login" }, constraints: null, namespaces: null);
            RouteTable.Routes.MapRoute("User2", "User/GetSchool", defaults: new { controller = "User", action = "GetSchool" }, constraints: null, namespaces: null);
        }
    }
}
