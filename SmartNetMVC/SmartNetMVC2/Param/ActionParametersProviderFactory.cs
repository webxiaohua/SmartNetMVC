using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Smart.NetMVC2
{
    /// <summary>
    /// 获取 Action 参数
    /// </summary>
    internal static class ActionParametersProviderFactory
    {
        public static IActionParamProvider CreateActionParamProvider(HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");
            string contentType = request.ContentType;
            if (contentType.IndexOf("application/x-www-form-urlencoded", StringComparison.OrdinalIgnoreCase) >= 0) { 
                return new 
            }
        }
    }
}
