using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;
using Smart.NetMVC2;

namespace TestWebApp
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            //RouteTable.Routes.MapPageRoute("User", "User/Login", "~/User/Login.cspx", false);
            //RouteTable.Routes.MapRoute("User", "User/Login", defaults: new { controller = "User", action = "Login" }, constraints: null, namespaces: null);
            URLRoute.RegisterRoutes();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //获取到HttpUnhandledException异常，这个异常包含一个实际出现的异常
            Exception ex = Server.GetLastError();
            Action<Exception> action = (a) =>
            {
                HttpContext.Current.Response.Write("服务器内部错误:" + a.Message);
            };
            SysHook.ApplicationErrorHandle(ex, action);
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 注册路由表
        /// </summary>
        /// <param name="routes"></param>
        public static void RegisterRoutes(RouteCollection routes)
        {


            //默认页
            //routes.MapPageRoute("Default", "", "~/Default.aspx");//主页
            //对{folder}/{webform}形式的URL进行路由
            //routes.MapPageRoute("WebForm1", "{controller}/{action}", "~/{controller}/{action}.aspx");
            //routes.MapPageRoute("WebForm1", "{controller}/{action}", "~/{controller}/{action}.htm");
            //routes.MapPageRoute("WebForm2", "{folder}/{webform}", "~/{folder}/{webform}.aspx");
            //routes.MapPageRoute("MustNumber", "index/{parameter}", "~/Defaults.aspx", false, new RouteValueDictionary { { "parameter", "有默认参数" } });

            //路由名称 传入的URL 路由后的URL 是否允许用户直接访问URL 参数无默认值 占位符需满足正则条件
            //routes.MapPageRoute("MustNumber", "index/{parameter}", "~/Defaults.aspx", false, null, new RouteValueDictionary { { "parameter", @"\d" } });


        }
    }
}