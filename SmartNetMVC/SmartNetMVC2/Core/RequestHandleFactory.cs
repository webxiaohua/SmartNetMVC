using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Smart.NetMVC2
{
    public class RequestHandleFactory : IHttpHandlerFactory
    {
        /// <summary>
        /// 获取请求处理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requestType"></param>
        /// <param name="url"></param>
        /// <param name="pathTranslated"></param>
        /// <returns></returns>
        public IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
        {
            //验证路径是否为请求普通资源
            if (url.EndsWith(".aspx"))
            {
                PageHandlerFactory factory = (PageHandlerFactory)Activator.CreateInstance(typeof(PageHandlerFactory), true);
                IHttpHandler handler = factory.GetHandler(context, requestType, url, pathTranslated);
                return handler;
            }
            else
            {
                string requestPath = context.Request.Path;//请求路径
                string vPath = UrlHelper.GetRealVirtualPath(context);//去除虚拟目录后得到的请求路径
                //尝试根据请求路径获取Action
                InvokeInfo vkInfo = InitEngine.GetInvokeInfo(vPath);
                if (vkInfo == null)
                    ExceptionHelper.Throw404Exception(context);
                if (vkInfo.Action.Attr != null && !vkInfo.Action.Attr.AllowExecute(context.Request.HttpMethod)) //限定谓词
                    ExceptionHelper.Throw403Exception(context);
                return ActionHandler.CreateHandler(vkInfo);

            }
        }
        /// <summary>
        /// 释放请求
        /// </summary>
        /// <param name="handler"></param>
        public void ReleaseHandler(IHttpHandler handler)
        {
            //throw new NotImplementedException();
        }
    }
}
