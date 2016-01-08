using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;

namespace Smart.NetMVC
{
    internal static class HttpExtensions
    {
        /// <summary>
        /// 读取请求数据流
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string ReadInputStream(this HttpRequest request)
        {
            request.InputStream.Position = 0;
            StreamReader sr = new StreamReader(request.InputStream, request.ContentEncoding);
            return sr.ReadToEnd();
        }
    }
}
