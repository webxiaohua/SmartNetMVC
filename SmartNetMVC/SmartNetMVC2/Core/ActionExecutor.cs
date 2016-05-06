using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Smart.NetMVC2
{
    /// <summary>
    /// Action 执行器
    /// </summary>
    public class ActionExecutor
    {
        //获取dll版本号
        private static readonly string Version = System.Diagnostics.FileVersionInfo.GetVersionInfo(typeof(ActionExecutor).Assembly.Location).FileVersion;

        private static void SetVersionHeader(HttpContext context)
        {
            context.Response.AppendHeader("X-SmartMVC-Version", Version);
        }

        internal static void ExecuteAction(HttpContext context, InvokeInfo vkInfo)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (vkInfo == null)
                throw new ArgumentNullException("vkInfo");
            //设置响应头
            SetVersionHeader(context);
            //调用Action方法
            object result = ExecuteActionInternal(context, vkInfo);
            
            if (result != null)
            {
                if (result is IActionResult)
                {  //返回视图模型
                    IActionResult executeResult = result as IActionResult;
                    executeResult.Output(context);
                }
                else
                {
                    //处理方法返回结果
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(result.ToString());
                }
            }
             
        }

        internal static object ExecuteActionInternal(HttpContext context, InvokeInfo vkInfo) {
            //准备要传给调用方法的参数
            object[] parameters = GetActionCallParameters(context, vkInfo.Action);
            //调用方法
            if (vkInfo.Action.HasReturn)
            {
                return vkInfo.Action.MethodInfo.Invoke(vkInfo.Instance, parameters);
            }
            else {
                vkInfo.Action.MethodInfo.Invoke(vkInfo.Instance, parameters);
                return null;
            }
        }

        private static object[] GetActionCallParameters(HttpContext context, ActionDescription action) {
            if (action.Parameters == null || action.Parameters.Length == 0) {
                return null;
            }
            IActionParamProvider provider = ActionParametersProviderFactory.CreateActionParamProvider(context.Request);
            return provider.GetParameters(context.Request, action);
        }
    }
}
