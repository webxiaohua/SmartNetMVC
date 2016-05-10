using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web;

namespace Smart.NetMVC2
{
    public class RouteHandler:IRouteHandler
    {
        public System.Web.IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new MvcHandler();
        }
    }

    public class MvcHandler : IHttpHandler {

        public bool IsReusable
        {
            get { throw new NotImplementedException(); }
        }

        public void ProcessRequest(HttpContext context)
        {
            string requestPath = context.Request.Path;//请求路径
            string vPath = UrlHelper.GetRealVirtualPath(context);//去除虚拟目录后得到的请求路径
            //尝试根据请求路径获取Action
            InvokeInfo vkInfo = InitEngine.GetInvokeInfo(vPath);
            if (vkInfo == null)
                ExceptionHelper.Throw404Exception(context);
            if (vkInfo.Action.Attr != null && !vkInfo.Action.Attr.AllowExecute(context.Request.HttpMethod)) //限定谓词
                ExceptionHelper.Throw403Exception(context);
            ActionHandler.CreateHandler(vkInfo).ProcessRequest(context);
        }
    }
    
}
