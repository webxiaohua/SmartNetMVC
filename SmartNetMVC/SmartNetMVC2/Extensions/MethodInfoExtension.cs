using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Smart.NetMVC2
{
    internal static class MethodInfoExtension
    {
        internal static T GetMyAttribute<T>(this MemberInfo m) where T : Attribute
        {
            return GetMyAttribute<T>(m, false);
        }

        internal static T GetMyAttribute<T>(this MemberInfo m, bool inherit) where T : Attribute
        {
            T[] array = m.GetCustomAttributes(typeof(T), inherit) as T[];

            if (array.Length == 1)
                return array[0];

            if (array.Length > 1)
                throw new InvalidProgramException(string.Format("方法 {0} 不能同时指定多次 [{1}]。", m.Name, typeof(T)));
            return null;
            //return default(T);
        }
    }
}
