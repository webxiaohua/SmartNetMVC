using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web;
using System.Web.UI;
using System.Web.SessionState;
using Smart.NetMVC2.Tool;
using Smart.NetMVC2.Const;

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
            if (vkInfo == null)
            {
                HttpContext.Current.Response.StatusCode = 404;
                HttpContext.Current.Response.Write("无法找到页面:" + context.Request.RawUrl);
            }
            else
            {
                string code = ValidateProcess(context, vkInfo);
                switch (code)
                {
                    case "200":
                        //执行aop
                        if (vkInfo.Controller.Injector != null)
                        {
                            vkInfo.Controller.ControllerContext = context;
                            vkInfo.Controller.Injector.OnActionExecuting(vkInfo.Controller);
                        }
                        ActionHandler.CreateHandler(vkInfo).ProcessRequest(context);
                        break;
                    case "403":
                        HttpContext.Current.Response.StatusCode = 403;
                        HttpContext.Current.Response.Write("权限不足");
                        break;
                    case "404":
                        HttpContext.Current.Response.StatusCode = 404;
                        HttpContext.Current.Response.Write("无法找到页面:" + context.Request.RawUrl);
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// 验证请求
        /// </summary>
        /// <param name="context"></param>
        /// <param name="vkInfo"></param>
        private string ValidateProcess(HttpContext context, InvokeInfo vkInfo)
        {
            string result = "200";//正常请求
            if (vkInfo == null)
                result = "404";
            //ExceptionHelper.Throw404Exception(context);
            if (vkInfo.Controller.AllowRole != null)
            {
                if (context.Request.Cookies[PubConst.Client_Unique_ID] != null)
                {  //校验用户身份
                    string smartIdentity = context.Request.Cookies[PubConst.Client_Unique_ID].Value;
                    smartIdentity = EncryptHelper.DESDeCode(smartIdentity);
                    if (CacheHelper<object>.GetInstance().ContainsKey(smartIdentity))
                    {
                        object val = CacheHelper<object>.GetInstance().Get(smartIdentity);
                        if (!vkInfo.Controller.AllowRole.AllowAccess(val))
                        {
                            //无权
                            //ExceptionHelper.Throw403Exception(context);
                            result = "403";
                        }
                    }
                    else
                    {
                        //无权
                        result = "403";
                        //ExceptionHelper.Throw403Exception(context);
                    }
                }
                else
                {
                    //没有权限
                    result = "403";
                    //ExceptionHelper.Throw403Exception(context);
                }
            }
            if (vkInfo.Controller.AllowUser != null)
            {
                if (context.Request.Cookies[PubConst.Client_Unique_ID] != null)
                {  //校验用户身份
                    string smartIdentity = context.Request.Cookies[PubConst.Client_Unique_ID].Value;
                    smartIdentity = EncryptHelper.DESDeCode(smartIdentity);
                    if (CacheHelper<object>.GetInstance().ContainsKey(smartIdentity))
                    {
                        object val = CacheHelper<object>.GetInstance().Get(smartIdentity);
                        if (!vkInfo.Controller.AllowUser.AllowAccess(val))
                        {
                            //无权
                            //ExceptionHelper.Throw403Exception(context);
                            result = "403";
                        }
                    }
                    else
                    {
                        //无权
                        //ExceptionHelper.Throw403Exception(context);
                        result = "403";
                    }
                }
                else
                {
                    //没有权限
                    //ExceptionHelper.Throw403Exception(context);
                    result = "403";
                }
            }
            if (vkInfo.Action.Attr != null && !vkInfo.Action.Attr.AllowExecute(context.Request.HttpMethod)) //限定谓词
                result = "403";
            //ExceptionHelper.Throw403Exception(context);

            return result;
            /*
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
             * */
        }
    }

}
