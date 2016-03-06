using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWebApp
{
    public class TiuHttpHandler : IHttpHandler
    {
        /// <summary>
        /// 这http handle的实例是否能被重用来处理多个同类型的http请求
        /// </summary>
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var currentResponse = context.Response;
            var outPutStr = string.Format("Hello Tiu");
            currentResponse.Write(outPutStr);
        }
    }
}