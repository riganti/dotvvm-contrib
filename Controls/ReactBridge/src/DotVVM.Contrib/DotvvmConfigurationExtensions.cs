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


        }
    }
}
