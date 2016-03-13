using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace Smart.NetMVC2
{
    public static class DynamicMethodFactory
    {
        public static Func<Object> CreateConstructor(ConstructorInfo constructor)
        {
            if (constructor == null)
                throw new ArgumentException("constructor");
            if (constructor.GetParameters().Length > 0)
                throw new NotSupportedException("不支持有参数的构造函数。");
            DynamicMethod dm = new DynamicMethod(
                "ctor",
                constructor.DeclaringType,
                Type.EmptyTypes,
                true);

            ILGenerator il = dm.GetILGenerator();
            il.Emit(OpCodes.Nop);
            il.Emit(OpCodes.Newobj, constructor);
            il.Emit(OpCodes.Ret);
            return (Func<Object>)dm.CreateDelegate(typeof(Func<Object>));
        }
    }
}
