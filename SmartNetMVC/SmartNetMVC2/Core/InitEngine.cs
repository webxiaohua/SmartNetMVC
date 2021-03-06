﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Web.Compilation;
using System.Collections;
using System.Web.Routing;
using Smart.NetMVC2.AOP;

namespace Smart.NetMVC2
{
    /// <summary>
    /// 初始化引擎
    /// </summary>
    internal class InitEngine
    {
        public static Dictionary<string, ControllerDescription> s_ControllerNameDict { get; private set; }//控制器集合
        public static Dictionary<string, ActionDescription> s_ControllerActionDict { get; private set; }//Action 集合
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
                    {
                        AllowRoleAttribute allowRole = null;
                        AllowUserAttribute allowUser = null;
                        IControllerInjector injector = null;
                        if (t.GetCustomAttributes(typeof(AllowRoleAttribute), false).Length != 0)
                        {
                            allowRole = (AllowRoleAttribute)t.GetCustomAttributes(typeof(AllowRoleAttribute), false)[0];
                        }
                        if (t.GetCustomAttributes(typeof(AllowUserAttribute), false).Length != 0)
                        {
                            allowUser = (AllowUserAttribute)t.GetCustomAttributes(typeof(AllowUserAttribute), false)[0];
                        }
                        if (t.GetCustomAttributes(typeof(IControllerInjector), false).Length != 0)
                        {
                            injector = (IControllerInjector)t.GetCustomAttributes(typeof(IControllerInjector), false)[0];
                        }
                        controllerList.Add(new ControllerDescription(t, allowRole, allowUser, injector));
                    }
                }
            }
            s_ControllerNameDict = controllerList.ToDictionary(x => x.ControllerType.Name, StringComparer.OrdinalIgnoreCase);
            // 提前加载Page Controller中的所有Action方法
            s_ControllerActionDict = new Dictionary<string, ActionDescription>();
            foreach (ControllerDescription controller in controllerList)
            {
                foreach (MethodInfo m in controller.ControllerType.GetMethods(ActionBindingFlags))
                {
                    if (m.Name.EndsWith("Action"))
                    {
                        ActionAttribute actionAttr = m.GetMyAttribute<ActionAttribute>();
                        AllowRoleAttribute allowRole = m.GetMyAttribute<AllowRoleAttribute>();
                        AllowUserAttribute allowUser = m.GetMyAttribute<AllowUserAttribute>();
                        IActionInjector injector = m.GetMyAttribute<IActionInjector>();
                        ActionDescription actionDescription = new ActionDescription(m, actionAttr, allowRole, allowUser, injector) { PageController = controller };
                        s_ControllerActionDict.Add(controller.ControllerType.Name.ToLower() + "_" + m.Name.ToLower(), actionDescription);
                    }
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
            if (url == "/")
            {
                if (RouteTable.Routes["Default"] != null)
                {
                    //string a = ((Route)(RouteTable.Routes["Default"]));
                    //url = url + ((Route)(RouteTable.Routes["Default"])).Defaults.Values["0"].ToString() + "/" + ((Route)(RouteTable.Routes["Default"])).Defaults.Values[1];
                }
            }
            url = url.StartsWith("/") ? url.Substring(1) : url;
            if (url.Contains('.'))
                url = url.Substring(0, url.IndexOf("."));
            string[] controllerActionPair = url.Split('/');
            string controllerName = (controllerActionPair[0] + "Controller").ToLower();
            string actionName = (controllerActionPair[1] + "Action").ToLower();
            if (s_ControllerActionDict.ContainsKey(controllerName + "_" + actionName))
            {
                ActionDescription action = s_ControllerActionDict[controllerName + "_" + actionName];
                InvokeInfo vkInfo = new InvokeInfo();
                vkInfo.Controller = action.PageController;
                vkInfo.Action = action;
                vkInfo.Instance = vkInfo.Controller.ControllerType.FastNew();
                return vkInfo;
            }
            else
            {
                return null;
            }
        }

    }
}
