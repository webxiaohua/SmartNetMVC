using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Smart.NetMVC2
{
    /// <summary>
    /// 动作执行返回结果
    /// </summary>
    public interface IActionResult
    {
        void Output(HttpContext context);
    }

    /// <summary>
    /// 返回视图结果
    /// </summary>
    public class PageResult : IActionResult
    {
        public string VirtualPath { get; set; }
        public object Model { get; set; }

        public PageResult(string virtualPath)
            : this(virtualPath, null)
        {
        }

        public PageResult(string virtualPath, object model)
        {
            this.VirtualPath = virtualPath;
            this.Model = model;
        }
        /// <summary>
        /// 输出视图结果
        /// </summary>
        /// <param name="context"></param>
        public void Output(HttpContext context)
        {
            if (string.IsNullOrEmpty(this.VirtualPath))
                this.VirtualPath = context.Request.FilePath;
            context.Response.ContentType = "text/html";
            string html = PageExecutor.Render(context, VirtualPath, Model);
            context.Response.Write(html);
        }
    }

    public class RedirectResult : IActionResult
    {
        public string RedirectUrl { get; private set; }
        public RedirectResult(string redirectUrl)
        {
            this.RedirectUrl = redirectUrl;
        }
        /// <summary>
        /// 输出结果
        /// </summary>
        /// <param name="context"></param>
        public void Output(HttpContext context)
        {
            context.Response.Redirect(RedirectUrl);
        }
    }
}
