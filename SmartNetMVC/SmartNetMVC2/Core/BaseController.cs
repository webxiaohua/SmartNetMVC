using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Smart.NetMVC2
{
    /// <summary>
    /// Controller 基类
    /// 使用它可以访问 HttpRequest  HttpResponse 等系统对象
    /// </summary>
    public class BaseController
    {
        public HttpContext HttpContext { get; set; }
    }
}
