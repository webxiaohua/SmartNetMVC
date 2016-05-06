using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Reflection;

namespace Smart.NetMVC2.Param
{
    internal static class ParamHelper
    {
        /// <summary>
        /// 从form 请求中获取参数信息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="parentName"></param>
        /// <returns></returns>
        public static object GetValueByNameAndTypeFromRequest(HttpRequest request,string name,Type type,string parentName) {
            MethodInfo stringImplicit = null;
            // 检查是否为不支持的参数类型
            if (type.IsSupportableType() == false)
            {
                // 检查是否可以做隐式类型转换  
                stringImplicit = GetStringImplicit(type);

                if (stringImplicit == null)
                    return null;
            }
            string[] val = GetValueFromHttpRequest(request, name, parentName);
            if (type == typeof(string[]))
                return val;
            if (val == null || val.Length == 0)
                return null;
            // 还原ASP.NET的默认数据格式
            string str = val.Length == 1 ? val[0] : string.Join(",", val);
            // 可以做隐式类型转换
            if (stringImplicit != null)
                return stringImplicit.FastInvoke(null, str.Trim());
        }

        /// <summary>
        /// 获取request 参数
        /// </summary>
        /// <param name="request"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static string[] GetHttpValues(HttpRequest request, string name)
        {
            string[] val = request.QueryString.GetValues(name);
            if (val == null)
                val = request.Form.GetValues(name);

            return val;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="name"></param>
        /// <param name="parentName"></param>
        /// <returns></returns>
        private static string[] GetValueFromHttpRequest(HttpRequest request, string name, string parentName) {
            string[] val = GetHttpValues(request, name);
            if (val == null) {
                // 再试一次。有可能是多个自定义类型，Form表单元素采用变量名做为前缀。
                if (string.IsNullOrEmpty(parentName) == false)
                {
                    val = GetHttpValues(request, parentName + "." + name);
                }
            }
            return val;
        }

        /// <summary>
        /// 判断指定类型是否能从String类型做隐式类型转换  如果可以，则返回响应的方法
        /// </summary>
        /// <param name="conversionType"></param>
        /// <returns></returns>
        private static MethodInfo GetStringImplicit(Type conversionType) {
            MethodInfo m = conversionType.GetMethod("op_Implicit", BindingFlags.Static | BindingFlags.Public); //隐式转换
            if (m != null && m.IsStatic && m.IsSpecialName && m.ReturnType == conversionType) {
                ParameterInfo[] paras = m.GetParameters();
                if (paras.Length == 1 && paras[0].ParameterType == typeof(string))
                    return m;
            }
            return null;
        }
    }
}
