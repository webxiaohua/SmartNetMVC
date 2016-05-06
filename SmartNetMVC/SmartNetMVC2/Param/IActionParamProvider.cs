using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Smart.NetMVC2
{
    /// <summary>
    /// 根据请求对象和 Action 获取参数
    /// </summary>
    internal interface IActionParamProvider
    {
        object[] GetParameters(HttpRequest request, ActionDescription action);
    }
}
