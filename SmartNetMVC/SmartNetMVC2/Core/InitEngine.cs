using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Web.Compilation;
using System.Collections;

namespace Smart.NetMVC2
{
    /// <summary>
    /// 初始化引擎
    /// </summary>
    internal class InitEngine
    {
        private static Dictionary<string, ControllerDescription> s_ControllerNameDict;//控制器集合
        private static Dictionary<string, ActionDescription> s_ControllerActionDict;//Action 集合
        // 用于从类型查找Action的反射标记
        private static readonly BindingFlags ActionBindingFlags =
            BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase;
        /// <summary>
        /// 初始化controller
        /// </summary>
        static InitEngine()
        {
            InitControllers();
        }

        /// <summary>
        /// 加载所有Controller
        /// </summary>
        private static void InitControllers()
        {
            List<ControllerDescription> controllerList = new List<ControllerDescription>();//控制器集合
            ICollection assemblies = BuildManager.GetReferencedAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                //过滤system开头的程序集，加快速度
                if (assembly.FullName.StartsWith("System", StringComparison.OrdinalIgnoreCase))
                    continue;
                foreach (Type t in assembly.GetExportedTypes())
                {
                    if (t.IsClass == false)
                        continue;
                    if (t.Name.EndsWith("Controller"))
                        controllerList.Add(new ControllerDescription(t));
                }
            }
            s_ControllerNameDict = controllerList.ToDictionary(x => x.ControllerType.Name, StringComparer.OrdinalIgnoreCase);
            // 提前加载Page Controller中的所有Action方法
            s_ControllerActionDict = new Dictionary<string, ActionDescription>();
            foreach (ControllerDescription controller in controllerList)
            {
                foreach (MethodInfo m in controller.ControllerType.GetMethods(ActionBindingFlags))
                {
                    ActionDescription actionDescription =
                            new ActionDescription(m) { PageController = controller };
                    s_ControllerActionDict.Add(controller.ControllerType.Name + "_" + m.Name, actionDescription);
                }
            }
        }

        /// <summary>
        /// 根据URL获取 调用信息
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static InvokeInfo GetInvokeInfo(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException("url");
            url = url.StartsWith("/") ? url.Substring(1) : url;
            url = url.Substring(0, url.IndexOf("."));
            string[] controllerActionPair = url.Split('/');
            string controllerName = controllerActionPair[0] + "Controller";
            string actionName = controllerActionPair[1];

            ActionDescription action = s_ControllerActionDict[controllerName + "_" + actionName];

            InvokeInfo vkInfo = new InvokeInfo();
            vkInfo.Controller = action.PageController;
            vkInfo.Action = action;
            vkInfo.Instance = vkInfo.Controller.ControllerType.FastNew();
            return vkInfo;
        }

    }
}
