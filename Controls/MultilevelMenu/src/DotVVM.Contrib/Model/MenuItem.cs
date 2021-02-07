using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Routing;
using DotVVM.Framework.ViewModel;
using Newtonsoft.Json;

namespace DotVVM.Contrib.Model
{
    public class MenuItem : IMenuItem
    {

        public string Text { get; set; }

        public string NavigateUrl { get; set; }
        
        public bool IsActive { get; set; }

        public List<MenuItem> ChildItems { get; } = new List<MenuItem>();

        [Bind(Direction.None)]
        IEnumerable<IMenuItem> IMenuItem.ChildItems => ChildItems;

        public static string BuildUrl(IDotvvmRequestContext context, string routeName, IDictionary<string, object> routeParams, IDictionary<string, object> queryStringParams = null, string urlSuffix = null, bool keepCurrentRouteParams = true, bool autoDetectIsActive = true)
        {
            var baseUrl = keepCurrentRouteParams
                ? context.Configuration.RouteTable[routeName].BuildUrl(context.Parameters, routeParams)
                : context.Configuration.RouteTable[routeName].BuildUrl(routeParams);

            var suffix = UrlHelper.BuildUrlSuffix(urlSuffix, queryStringParams);

            return context.TranslateVirtualPath(baseUrl + suffix);
        }

        public static string BuildUrl(IDotvvmRequestContext context, string routeName, object routeParams = null, object queryStringParams = null, string urlSuffix = null, bool keepCurrentRouteParams = true)
        {
            var baseUrl = keepCurrentRouteParams
                ? context.Configuration.RouteTable[routeName].BuildUrl(context.Parameters, routeParams)
                : context.Configuration.RouteTable[routeName].BuildUrl(routeParams);

            var suffix = UrlHelper.BuildUrlSuffix(urlSuffix, queryStringParams);

            return context.TranslateVirtualPath(baseUrl + suffix);
        }
        
    }

    public class MenuItem<T> : IMenuItem where T : class, new()
    {

        public string Text { get; set; }

        public string NavigateUrl { get; set; }

        public bool IsActive { get; set; }

        public List<MenuItem<T>> ChildItems { get; } = new List<MenuItem<T>>();
        
        public T ExtraData { get; set; }

        [Bind(Direction.None)] 
        IEnumerable<IMenuItem> IMenuItem.ChildItems => ChildItems;
    }
}
