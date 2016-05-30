using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web;
using System.Web.UI;
using System.Web.SessionState;

namespace Smart.NetMVC2
{
    public class RouteHandler : IRouteHandler
    {
        public System.Web.IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new MvcHandler();
        }
    }

    public class MvcHandler : IHttpHandler, IRequiresSessionState
    {

        public bool IsReusable
        {
            get { throw new NotImplementedException(); }
        }

        public void ProcessRequest(HttpContext context)
        {
            //验证路径是否为请求普通资源
            if (context.Request.RawUrl.EndsWith(".aspx"))
            {
                PageHandlerFactory factory = (PageHandlerFactory)Activator.CreateInstance(typeof(PageHandlerFactory), true);
                IHttpHandler handler = factory.GetHandler(context, context.Request.RequestType, context.Request.Url.AbsolutePath, context.Request.PhysicalApplicationPath);
                handler.ProcessRequest(context);
            }
            string requestPath = context.Request.Path;//请求路径
            string vPath = UrlHelper.GetRealVirtualPath(context);//去除虚拟目录后得到的请求路径

            //尝试根据请求路径获取Action
            InvokeInfo vkInfo = InitEngine.GetInvokeInfo(vPath);
            ValidateProcess(context, vkInfo);
            ActionHandler.CreateHandler(vkInfo).ProcessRequest(context);
        }
        /// <summary>
        /// 验证请求
        /// </summary>
        /// <param name="context"></param>
        /// <param name="vkInfo"></param>
        private void ValidateProcess(HttpContext context, InvokeInfo vkInfo)
        {
            if (vkInfo == null)
                ExceptionHelper.Throw404Exception(context);
            if (vkInfo.Controller.AllowRole != null)
            {
                if (context.Session != null && context.Session["SmartMVC_Current_UserRole"] != null)
                {
                    if (!vkInfo.Controller.AllowRole.AllowAccess(context.Session["SmartMVC_Current_UserRole"]))
                        ExceptionHelper.Throw403Exception(context);
                }
                else {
                    ExceptionHelper.Throw403Exception(context);
                }
            }
            if (vkInfo.Controller.AllowUser != null)
            {
                if (context.Session != null && context.Session["SmartMVC_Current_UserIdentity"] != null)
                {
                    if (!vkInfo.Controller.AllowUser.AllowAccess(context.Session["SmartMVC_Current_UserIdentity"]))
                        ExceptionHelper.Throw403Exception(context);
                }
                else
                {
                    ExceptionHelper.Throw403Exception(context);
                }
            }
            if (vkInfo.Action.Attr != null && !vkInfo.Action.Attr.AllowExecute(context.Request.HttpMethod)) //限定谓词
                ExceptionHelper.Throw403Exception(context);
            if (vkInfo.Action.AllowRole != null)
            {
                if (context.Session != null && context.Session["SmartMVC_Current_UserRole"] != null)
                {
                    if (!vkInfo.Action.AllowRole.AllowAccess(context.Session["SmartMVC_Current_UserRole"]))
                        ExceptionHelper.Throw403Exception(context);
                }
                else
                {
                    ExceptionHelper.Throw403Exception(context);
                }
            }
            if (vkInfo.Action.AllowUser != null)
            {
                if (context.Session != null && context.Session["SmartMVC_Current_UserIdentity"] != null)
                {
                    if (!vkInfo.Action.AllowUser.AllowAccess(context.Session["SmartMVC_Current_UserIdentity"]))
                        ExceptionHelper.Throw403Exception(context);
                }
                else
                {
                    ExceptionHelper.Throw403Exception(context);
                }
            }
        }
    }

}
