using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.Contrib
{
    public static class CookieBarConfigurationExtensions
    {

        public static void AddContribCookieBarConfiguration(this DotvvmConfiguration config)
        {
            config.Markup.AddMarkupControl("dc", nameof(CookieBar), $"embedded://{typeof(CookieBar).Assembly.GetName().Name}/{nameof(CookieBar)}.dotcontrol");
            config.Markup.AddCodeControls("dc", "DotVVM.Contrib", "DotVVM.Contrib.CookieBar");

            config.Resources.Register("dotvvm.contrib.CookieBar", new ScriptModuleResource(new EmbeddedResourceLocation(typeof(CookieBar).GetTypeInfo().Assembly, "DotVVM.Contrib.Scripts.DotVVM.Contrib.CookieBar.js"))
            {
                Dependencies = new [] { "dotvvm", "dotvvm.contrib.CookieBar.css" }
            });
            config.Resources.Register("dotvvm.contrib.CookieBar.css", new StylesheetResource()
            {
                Location = new EmbeddedResourceLocation(typeof(CookieBar).GetTypeInfo().Assembly, "DotVVM.Contrib.Styles.DotVVM.Contrib.CookieBar.min.css")
            });
        }

    }
}
