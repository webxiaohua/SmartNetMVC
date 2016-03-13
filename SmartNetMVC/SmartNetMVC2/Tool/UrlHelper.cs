using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Smart.NetMVC2
{
    /// <summary>
    /// URL处理类
    /// </summary>
    public class UrlHelper
    {
        /// <summary>
        /// 获取实际的虚拟路径 如果网站部署在虚拟目录中，将去除虚拟目录的顶层目录名 从而得到Controller 和 Action
        /// </summary>
        /// <param name="context"></param>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        public static string GetRealVirtualPath(HttpContext context)
        {
            string virtualPath = context.Request.Path;
            if (context.Request.ApplicationPath != "/")
            {
                //存在虚拟路径
                if (virtualPath.StartsWith(context.Request.ApplicationPath + "/"))
                    return virtualPath.Substring(context.Request.ApplicationPath.Length); //移除 '/'
            }
            return virtualPath;//不包含虚拟目录
        }
    }
}
