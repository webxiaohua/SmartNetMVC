using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Reflection;

namespace Smart.NetMVC2
{
    public class ModelHelper
    {
        /// <summary>
        /// 根据HttpRequest 对象获取param
        /// </summary>
        /// <param name="request"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="parentName"></param>
        /// <returns></returns>
        public static object GetValueByNameAndTypeFromRequest(HttpRequest request, string name, Type type, string parentName)
        {
            MethodInfo stringImplicit = null;
            //检查是否为不支持的参数类型
            if (type.IsSupportableType() == false)
            {
                //检查参数是否可以做隐式类型转换
                stringImplicit = GetStringImplicit(type);
                if (stringImplicit == null)
                    return null;
            }
            string val = GetValueFromHttpRequest(request, name, parentName);
            object result = null;
            try
            {
                result = Convert.ChangeType(val, type);
            }
            catch (Exception)
            {
            }
            return result;
        }

        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="request"></param>
        /// <param name="name"></param>
        /// <param name="parentName"></param>
        /// <returns></returns>
        public static string GetValueFromHttpRequest(HttpRequest request, string name, string parentName)
        {
            string val = GetHttpValues(request, name);
            if (val == null)
            {
                // 再试一次。有可能是多个自定义类型，Form表单元素采用变量名做为前缀。
                if (string.IsNullOrEmpty(parentName) == false)
                {
                    val = GetHttpValues(request, parentName + "." + name);
                }
            }
            return val;
        }

        private static string GetHttpValues(HttpRequest request, string name)
        {
            string val = request.QueryString.Get(name);
            if (val == null)
                val = request.Form.Get(name);

            return val;
        }

        /// <summary>
        /// 判断指定的类型是否能从String类型做隐式类型转换，如果可以，则返回相应的方法
        /// </summary>
        /// <param name="conversionType"></param>
        /// <returns></returns>
        public static MethodInfo GetStringImplicit(Type conversionType)
        {
            MethodInfo m = conversionType.GetMethod("op_Implicit", BindingFlags.Static | BindingFlags.Public);
            if (m != null && m.IsStatic && m.IsSpecialName && m.ReturnType == conversionType)
            {
                ParameterInfo[] paras = m.GetParameters();
                if (paras.Length == 1 && paras[0].ParameterType == typeof(string))
                    return m;
            }
            return null;
        }
    }
}
