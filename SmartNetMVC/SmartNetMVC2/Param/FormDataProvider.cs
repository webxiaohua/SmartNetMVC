using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections.Specialized;

namespace Smart.NetMVC2
{
    internal class FormDataProvider:IActionParamProvider
    {
        public object[] GetParameters(System.Web.HttpRequest request, ActionDescription action)
        {
            if (request == null)
                throw new ArgumentNullException("request");
            if (action == null)
                throw new ArgumentNullException("action");
            object[] parameters = new object[action.Parameters.Length];
            for (int i = 0; i < action.Parameters.Length; i++)
            {
                ParameterInfo p = action.Parameters[i];
                if (p.IsOut)
                    continue;
                Type paramterType = p.ParameterType.GetRealType();
                //如果参数是可支持的类型，直接从HttpRequest中读取并赋值
                if (paramterType.IsSupportableType()) { 
                    
                }
            }
        }
    }
}
