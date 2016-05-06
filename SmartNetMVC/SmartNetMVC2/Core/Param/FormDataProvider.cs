using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Web;
using System.Collections.Specialized;

namespace Smart.NetMVC2
{
    internal class FormDataProvider:IActionParamProvider
    {
        public object[] GetParameters(HttpRequest request, ActionDescription action)
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
                if (p.ParameterType == typeof(NameValueCollection))
                {
                    if (string.Compare(p.Name, "Form", StringComparison.OrdinalIgnoreCase) == 0)
                        parameters[i] = request.Form;
                    else if (string.Compare(p.Name, "QueryString", StringComparison.OrdinalIgnoreCase) == 0)
                        parameters[i] = request.QueryString;
                }
                else {
                    Type paramterType = p.ParameterType.GetRealType();
                    //
                    if (paramterType.IsSupportableType())
                    {
                        object val = ModelHelper.GetValueByNameAndTypeFromRequest(request, p.Name, paramterType, null);
                        if (val != null)
                            parameters[i] = val;
                        else
                        {
                            if (p.ParameterType.IsValueType && p.ParameterType.IsNullableType() == false)
                                throw new ArgumentException("未能找到指定的参数值：" + p.Name);
                        }
                    }
                    else {
                        // 自定义的类型。首先创建实例，然后给所有成员赋值。  暂不支持
                        // 注意：这里不支持嵌套类型的自定义类型。
                        //object item = Activator.CreateInstance(paramterType);
                        
                    }
                }
            }
            return parameters;
        }
    }
}
