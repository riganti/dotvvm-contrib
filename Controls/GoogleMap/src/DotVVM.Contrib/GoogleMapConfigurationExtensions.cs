using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.Contrib
{
    public static class GoogleMapConfigurationExtensions
    {
        public static void AddContribGoogleMapConfiguration(this DotvvmConfiguration config,string googleApiKey)
        {
            // register tag prefix
            if (config.Markup.Controls.All(c => c.TagPrefix != "dc"))
            {
                config.Markup.Controls.Add(new DotvvmControlConfiguration()
                {
                    Assembly = "DotVVM.Contrib.GoogleMap",
                    Namespace = "DotVVM.Contrib",
                    TagPrefix = "dc"
                });
            }

            config.Resources.Register("dotvvm.contrib.GoogleMap.GoogleCode", new ScriptResource(
                new UrlResourceLocation($@"https://maps.googleapis.com/maps/api/js?key={googleApiKey}"))
            {
                RenderPosition = ResourceRenderPosition.Body
            });

            // register additional resources for the control and set up dependencies
            config.Resources.Register("dotvvm.contrib.GoogleMap", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(typeof(GoogleMap).GetTypeInfo().Assembly, "DotVVM.Contrib.Scripts.DotVVM.Contrib.GoogleMap.js"),
                Dependencies = new [] { "dotvvm", "dotvvm.contrib.GoogleMap.GoogleCode" }
            });

        }

    }
}
