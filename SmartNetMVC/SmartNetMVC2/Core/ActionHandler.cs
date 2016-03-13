using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Smart.NetMVC2
{
    /// <summary>
    /// 创建Action
    /// </summary>
    public class ActionHandler : IHttpHandler
    {
        InvokeInfo InvokeInfo;
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            // 调用核心的工具类，执行Action
            ActionExecutor.ExecuteAction(context, this.InvokeInfo);
        }

        public static ActionHandler CreateHandler(InvokeInfo vkInfo)
        {
            return new ActionHandler { InvokeInfo = vkInfo };
        }
    }
}
