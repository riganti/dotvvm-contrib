using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.Contrib
{
    public static class DotvvmConfigurationExtensions
    {

        public static void AddReactBridgeConfiguration(this DotvvmConfiguration config)
        {
            config.Markup.Controls.Add(new DotvvmControlConfiguration()
            {
                Assembly = typeof(ReactBridge).Assembly.GetName().Name,
                Namespace = typeof(ReactBridge).Namespace,
                TagPrefix = "dc"
            });

            // register additional resources for the control and set up dependencies
            config.Resources.Register("ReactBridge", new ScriptResource(
                new EmbeddedResourceLocation(typeof(ReactBridge).GetTypeInfo().Assembly, "DotVVM.Contrib.Scripts.DotVVM.Contrib.ReactBridge.js"))
            {
                Dependencies = new[] { "dotvvm" }
            });

            config.Resources.Register("react", new ScriptResource(new UrlResourceLocation("https://unpkg.com/react@15/dist/react.js")));
            config.Resources.Register("react-dom", new ScriptResource(new UrlResourceLocation("https://unpkg.com/react-dom@15/dist/react-dom.js")) { Dependencies = new[] { "react" } });
            config.Resources.Register("react-trend", new ScriptResource(new UrlResourceLocation("https://unpkg.com/react-trend@1.2.4/umd/react-trend.js")) { Dependencies = new[] { "react" } });
            config.Resources.Register("prop-types-DD", new ScriptResource(new UrlResourceLocation("https://unpkg.com/prop-types/prop-types.js")) { Dependencies = new[] { "react" } });
            config.Resources.Register("prop-types", new InlineScriptResource()
            {
                Code = "window['prop-types'] = window['PropTypes']",
                Dependencies = new[] { "prop-types-DD" }
            });
            config.Resources.Register("react-numeric-input",
                new ScriptResource(new UrlResourceLocation("https://unpkg.com/react-numeric-input@2.1.0/dist/react-numeric-input.js"))
                {
                    Dependencies = new[] { "react", "prop-types", "ReactBridge" }
                });


            config.Resources.Register("chartist-css",
               new StylesheetResource(new EmbeddedResourceLocation(typeof(Chartist).GetTypeInfo().Assembly, "DotVVM.Contrib.Styles.chartist.min.css")));

            config.Resources.Register("chartist-js",
         new ScriptResource(new UrlResourceLocation("https://cdn.jsdelivr.net/chartist.js/latest/chartist.min.js"))
         {
             LocationFallback = new ResourceLocationFallback(
                 "window.Chartist",
                 new EmbeddedResourceLocation(typeof(Chartist).GetTypeInfo().Assembly, "DotVVM.Contrib.Scripts.chartist.min.js")),
             Dependencies = new[] { "chartist-css" }
         });

            config.Resources.Register("ReactChartist", new ScriptResource(
                new EmbeddedResourceLocation(typeof(Chartist).GetTypeInfo().Assembly, "DotVVM.Contrib.Scripts.ReactChartis.js"))
            {
                Dependencies = new[] { "dotvvm", "react", "prop-types", "chartist-js", "ReactBridge" }
            });
        }

    }
}
