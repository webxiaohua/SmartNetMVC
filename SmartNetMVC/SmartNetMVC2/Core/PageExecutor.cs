using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.Compilation;
using System.IO;

namespace Smart.NetMVC2
{
    /// <summary>
    /// View执行器
    /// </summary>
    public class PageExecutor
    {
        /// <summary>
        /// 加载页面代码，生成html
        /// </summary>
        /// <param name="context"></param>
        /// <param name="pageVirthalPath"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string Render(HttpContext context, string pageVirtualPath, object model) {
            if (context == null)
                throw new ArgumentNullException("context");
            if (string.IsNullOrEmpty(pageVirtualPath))
                throw new ArgumentNullException("pageVirtualPath");
            Page handler = BuildManager.CreateInstanceFromVirtualPath(pageVirtualPath, typeof(object)) as Page;
            if (handler == null)
                throw new InvalidOperationException(string.Format("指定路径 {0} 不是一个有效的页面", pageVirtualPath));
            //为Page设置model
            SetPageModel(handler, model);
            StringWriter output = new StringWriter();
            context.Server.Execute(handler, output, false);
            return output.ToString();
        }

        private static void SetPageModel(IHttpHandler pageHandler, object model) {
            if (pageHandler == null)
                return;
            if (model != null)
            {
                BasePage page = pageHandler as BasePage;
                if (page != null)
                    page.SetModel(model);

            }
        }

    }
}
