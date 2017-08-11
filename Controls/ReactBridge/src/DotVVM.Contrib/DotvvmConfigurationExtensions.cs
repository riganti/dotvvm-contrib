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
            // register tag prefix
            if (!config.Markup.Controls.Any(c => c.TagPrefix == "dc"))
            {
                config.Markup.Controls.Add(new DotvvmControlConfiguration()
                {
                    Assembly = "DotVVM.Contrib",
                    Namespace = "DotVVM.Contrib",
                    TagPrefix = "dc"
                });
            }
            
            // register additional resources for the control and set up dependencies
            config.Resources.Register("ReactBridge", new ScriptResource(
                new EmbeddedResourceLocation(typeof(ReactBridge).GetTypeInfo().Assembly, "DotVVM.Contrib.Scripts.DotVVM.Contrib.ReactBridge.js"))
            {
                Dependencies = new [] { "dotvvm" }
            });
            // config.Resources.Register("dotvvm.contrib.ReactBridge.css", new ScriptResource(new EmbeddedResourceLocation(typeof(ReactBridge).GetTypeInfo().Assembly, "DotVVM.Contrib.Styles.DotVVM.Contrib.ReactBridge.css")));

            config.Resources.Register("react", new ScriptResource(new UrlResourceLocation("https://unpkg.com/react@15/dist/react.js")));
            config.Resources.Register("react-dom", new ScriptResource(new UrlResourceLocation("https://unpkg.com/react-dom@15/dist/react-dom.js")) { Dependencies = new[] { "react" } });
            config.Resources.Register("react-trend", new ScriptResource(new UrlResourceLocation("https://unpkg.com/react-trend@1.2.4/umd/react-trend.js")) { Dependencies = new[] { "react" } });
            config.Resources.Register("prop-types-DD", new ScriptResource(new UrlResourceLocation("https://unpkg.com/prop-types/prop-types.js")) { Dependencies = new[] { "react" } });
            config.Resources.Register("prop-types", new InlineScriptResource(){
                Code = "window['prop-types'] = window['PropTypes']",
                Dependencies = new [] { "prop-types-DD" }
            });
            config.Resources.Register("react-numeric-input", new ScriptResource(new UrlResourceLocation("https://unpkg.com/react-numeric-input@2.1.0/dist/react-numeric-input.js")) { Dependencies = new[] { "react", "prop-types" } });
            // config.Resources.Register("Recharts", new ScriptResource(new UrlResourceLocation("https://unpkg.com/recharts/umd/Recharts.min.js")) { Dependencies = new[] { "react" } });
        }

    }
}
