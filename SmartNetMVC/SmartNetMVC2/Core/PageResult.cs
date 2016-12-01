using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

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

    /// <summary>
    /// 输出JS
    /// </summary>
    public class JavaScriptResult : IActionResult
    {
        public string Content { get; set; }
        public JavaScriptResult(string content)
        {
            this.Content = content;
        }
        public void Output(HttpContext context)
        {
            //context.Response.ContentType = "text/javascript";
            context.Response.Write(this.Content);
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

    /// <summary>
    /// 输出文本结果
    /// </summary>
    public class TextResult : IActionResult
    {
        public Encoding Encoding { get; set; }
        public string Text { get; set; }
        public TextResult(string text)
        {
            this.Text = text;
        }
        public TextResult(string text, Encoding encoding)
        {
            this.Text = text;
            this.Encoding = encoding;
        }
        /// <summary>
        /// 输出结果
        /// </summary>
        /// <param name="context"></param>
        public void Output(HttpContext context)
        {
            if (Encoding != null)
                context.Response.ContentEncoding = Encoding;
            context.Response.Write(Text);
        }
    }

    public class JsonResult : IActionResult
    {
        public object JsonObj { get; set; }
        public JsonResult(object jsonObj)
        {
            this.JsonObj = jsonObj;
        }
        /// <summary>
        /// 输出结果
        /// </summary>
        /// <param name="context"></param>
        public void Output(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            JavaScriptSerializer jss = new JavaScriptSerializer();
            context.Response.Write(jss.Serialize(JsonObj));
        }
    }
}
