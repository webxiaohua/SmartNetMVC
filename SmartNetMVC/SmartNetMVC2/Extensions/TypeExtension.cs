using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Smart.NetMVC2
{
    internal static class TypeExtension
    {

        public static Type GetRealType(this Type type)
        {
            if (type.IsGenericType)
                return Nullable.GetUnderlyingType(type) ?? type;
            else
                return type;
        }
        /// <summary>
        /// 是否支持这些类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsSupportableType(this Type type)
        {
            return type.IsPrimitive || type == typeof(string) || type == typeof(DateTime)
                || type == typeof(decimal) || type == typeof(Guid) || type.IsEnum || type == typeof(string[]);
        }

        /// <summary>
        /// 实例化一个对象
        /// </summary>
        /// <param name="instanceType"></param>
        /// <returns></returns>
        public static object FastNew(this Type instanceType) {
            if (instanceType == null)
                throw new ArgumentException("instanceType");
            ConstructorInfo ctorInfo = instanceType.GetConstructor(Type.EmptyTypes);
            Func<Object> ctor = DynamicMethodFactory.CreateConstructor(ctorInfo); //需要使用缓存解决反射性能问题
            return ctor();
        }
    }
}
