using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Globalization;

namespace Smart.NetMVC2
{
    public class LowerCaseUrlRoute : Route
    {
        private static readonly string[] requiredKeys = new[] { "area", "controller", "action" };

        public LowerCaseUrlRoute(string url, IRouteHandler routeHandler)
            : base(url, routeHandler) { }

        public LowerCaseUrlRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
            : base(url, defaults, routeHandler) { }

        public LowerCaseUrlRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
            : base(url, defaults, constraints, routeHandler) { }
        public LowerCaseUrlRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler)
            : base(url, defaults, constraints, dataTokens, routeHandler) { }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            LowerRouteValues(requestContext.RouteData.Values);
            LowerRouteValues(values);
            LowerRouteValues(Defaults);
            return base.GetVirtualPath(requestContext, values);
        }

        private void LowerRouteValues(RouteValueDictionary values)
        {
            foreach (var key in requiredKeys)
            {
                if (values.ContainsKey(key) == false) continue;

                var value = values[key];
                if (value == null) continue;

                var valueString = Convert.ToString(value, CultureInfo.InvariantCulture);
                if (valueString == null) continue;

                values[key] = valueString.ToLower();
            }

            var otherKyes = values.Keys
                .Except(requiredKeys, StringComparer.InvariantCultureIgnoreCase)
                .ToArray();

            foreach (var key in otherKyes)
            {
                var value = values[key];
                values.Remove(key);
                values.Add(key.ToLower(), value);
            }
        }
    }


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
            LowerCaseUrlRoute route = new LowerCaseUrlRoute(url, new RouteHandler());
            route.Defaults = new RouteValueDictionary(defaults);
            route.Constraints = new RouteValueDictionary(constraints);
            route.DataTokens = new RouteValueDictionary();
            if (namespaces != null && namespaces.Length > 0)
            {
                route.DataTokens["Namespaces"] = namespaces;
            }
            routes.Add(name, route);
            /*
            //这里的RouteHandler就是一个重要的切入点
            Route route = new Route(url, new RouteHandler())
            {
                Defaults = new RouteValueDictionary(defaults),
                Constraints = new RouteValueDictionary(constraints),
                DataTokens = new RouteValueDictionary()
            };
            //而这里的DataTokens仅仅只是作为附加的参数
            //作为后面搜索控制器时的一个条件
            if (namespaces != null && namespaces.Length > 0)
            {
                route.DataTokens["Namespaces"] = namespaces;
            }
            routes.Add(name, route);
             * */
            return route;
        }

        public static void IgnoreRoute(this RouteCollection routes, string url)
        {
            routes.Add(new Route(url, new StopRoutingHandler()));
        }
    }
}
