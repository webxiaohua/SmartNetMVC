using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Web;

namespace Smart.NetMVC2
{
    /// <summary>
    /// 文件数据
    /// </summary>
    public class FileDataProvider : IActionParamProvider
    {
        public object[] GetParameters(System.Web.HttpRequest request, ActionDescription action)
        {
            if (request == null)
                throw new ArgumentNullException("request");
            if (action == null)
                throw new ArgumentNullException("action");
            object[] parameters = new object[action.Parameters.Length];
            for (int i = 0; i < request.Files.Count; i++)
            {
                parameters[i] = request.Files[i];
            }
            return parameters;
        }
    }
}
