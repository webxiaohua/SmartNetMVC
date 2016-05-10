using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;

namespace Smart.NetMVC2
{
    public static class RouteExtension
    {
        public static Route MapRoute(this RouteCollection routes, string name, string url, object defaults, object constraints, string[] namespaces)
        {
            if (routes == null)
            {
                throw new ArgumentNullException("routes");
            }
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }
            //这里的RouteHandler就是一个重要的切入点
            Route route = new Route(url, new RouteHandler())
            {
                Defaults = new RouteValueDictionary(defaults),
                Constraints = new RouteValueDictionary(constraints),
                DataTokens = new RouteValueDictionary()
            };
            //而这里的DataTokens仅仅只是作为附加的参数
            //作为后面搜索控制器时的一个条件
            if (namespaces != null && namespaces.Length > 0) {
                route.DataTokens["Namespaces"] = namespaces;
            }
            routes.Add(name, route);
            return route;
        }
    }
}
