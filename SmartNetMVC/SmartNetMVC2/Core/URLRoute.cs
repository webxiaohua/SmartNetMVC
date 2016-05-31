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
            /*
            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            RouteTable.Routes.MapRoute("Default", "{controller}/{action}", defaults: new { controller = "User", action = "Login" }, constraints: null, namespaces: null);
            */

            RouteTable.Routes.MapRoute("Default", "{controller}/{action}", new { controller = "", action = "" }, null, null); //默认处理  需要在web.config 配置节点
            foreach (string key in InitEngine.s_ControllerActionDict.Keys)
            {
                string[] controllerAction = key.Split('_');
                RouteTable.Routes.MapRoute(key, controllerAction[0].Replace("Controller", "") + "/" + controllerAction[1].Replace("Action", ""), defaults: null, constraints: null, namespaces: null);
            }
            //RouteTable.Routes.MapRoute("SmartMVC_IndexPage", "/", defaults: null, constraints: null, namespaces: null);
            /*
            RouteTable.Routes.MapRoute("User", "User/Login", defaults: new { controller = "User", action = "Login" }, constraints: null, namespaces: null);
            RouteTable.Routes.MapRoute("User2", "User/GetSchool", defaults: new { controller = "User", action = "GetSchool" }, constraints: null, namespaces: null);
             */
        }
    }
}
