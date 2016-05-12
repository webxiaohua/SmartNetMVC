using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Smart.NetMVC2
{
    /// <summary>
    /// 提供给外部系统调用
    /// </summary>
    public class SysHook
    {
        /// <summary>
        /// 处理系统异常 Application_Error 调用
        /// </summary>
        /// <param name="ex"></param>
        public static void ApplicationErrorHandle(Exception ex, Action<Exception> action)
        {
            HttpException httpEx = ex as HttpException;
            if (httpEx != null)
            {
                if (httpEx.GetHttpCode() == 404)
                {
                    HttpContext.Current.Response.StatusCode = 404;
                    HttpContext.Current.Response.Write("无法找到页面");
                }
                else if (httpEx.GetHttpCode() == 403)
                {
                    HttpContext.Current.Response.StatusCode = 403;
                    HttpContext.Current.Response.Write("禁止访问");
                }
                else
                {
                    HttpContext.Current.Response.Write("服务器内部错误:" + httpEx.Message);
                }
            }
            else
            {
                action.Invoke(ex);
            }
            HttpContext.Current.Response.End();
        }
    }
}
