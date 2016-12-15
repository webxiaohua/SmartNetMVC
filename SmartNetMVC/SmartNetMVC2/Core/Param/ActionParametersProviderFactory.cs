using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Smart.NetMVC2
{
    internal static class ActionParametersProviderFactory
    {
        public static IActionParamProvider CreateActionParamProvider(HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }
            string contentType = request.ContentType;
            if (contentType.IndexOf("application/x-www-form-urlencoded", StringComparison.OrdinalIgnoreCase) >= 0)
                return new FormDataProvider();

            if (contentType.IndexOf("application/json", StringComparison.OrdinalIgnoreCase) >= 0)
                return new JsonDataProvider();
            if (contentType.IndexOf("application/octet-stream", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return new BinaryDataProvider();
            }
            if (contentType.IndexOf("multipart/form-data", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return new FileDataProvider();
            }
            // 默认还是表单的 key = vlaue格式。
            return new FormDataProvider();
        }
    }
}
