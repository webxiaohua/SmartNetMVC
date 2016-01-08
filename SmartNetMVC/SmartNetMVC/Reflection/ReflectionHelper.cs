using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smart.NetMVC.Reflection
{
    // 说明：
    // 1. 对于处理页面请求的Controller类型，这里不缓存，只缓存所包含的Action
    //    用于页面请求的Action的描述中，已包含Controller的描述信息。
    // 2. 对于处理Service请求的Controller以及Action，在初始化时，只查找出Controller，Action采用延迟加载方式

    // 原因：
    // 1. 页面请求是在Action中指定的，因此，只能先找到所有能处理页面请求的Action
    // 2. Ajax调用时，可以从URL中解析出要调用的类名与方法名，因此可以在调用时再去查找。

    internal static class ReflectionHelper
    {
        public static readonly Type VoidType = typeof(void);

        // 保存PageAction的字典
        private static Dictionary<string, ActionDescription> s_PageActionDict;


    }
}
