using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Smart.NetMVC2
{
    public sealed class ActionDescription
    {
        /// <summary>
        /// Action的Controller
        /// </summary>
        public ControllerDescription PageController; //为PageAction保留
        /// <summary>
        /// Action 的参数
        /// </summary>
        public ParameterInfo[] Parameters { get; private set; }

        /// <summary>
        /// Action 是否有返回值
        /// </summary>
        public bool HasReturn { get; private set; }

        /// <summary>
        /// Action的方法体信息
        /// </summary>
        public MethodInfo MethodInfo { get; private set; }

        public ActionAttribute Attr { get; private set; }

        public ActionDescription(MethodInfo methodInfo,ActionAttribute attr) {
            this.MethodInfo = methodInfo;
            this.Parameters = methodInfo.GetParameters();
            this.HasReturn = methodInfo.ReturnType != typeof(void);
            this.Attr = attr;
        }
    }
}
