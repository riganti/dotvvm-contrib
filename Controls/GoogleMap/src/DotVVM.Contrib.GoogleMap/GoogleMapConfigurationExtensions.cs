using System.Reflection;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.Contrib.GoogleMap
{
    public static class GoogleMapConfigurationExtensions
    {
        public static void AddContribGoogleMapConfiguration(this DotvvmConfiguration config,string googleApiKey)
        {

            config.Markup.Controls.Add(new DotvvmControlConfiguration()
            {
                Assembly = typeof(GoogleMap).Assembly.GetName().Name,
                Namespace = typeof(GoogleMap).Namespace,
                TagPrefix = "dc"
            });

            config.Resources.Register("dotvvm.contrib.GoogleMap.GoogleCode", new ScriptResource(
                new UrlResourceLocation($@"https://maps.googleapis.com/maps/api/js?key={googleApiKey}"))
            {
                RenderPosition = ResourceRenderPosition.Body
            });

            // register additional resources for the control and set up dependencies
            config.Resources.Register("dotvvm.contrib.GoogleMap", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(typeof(GoogleMap).GetTypeInfo().Assembly, "DotVVM.Contrib.GoogleMap.Scripts.DotVVM.Contrib.GoogleMap.js"),
                Dependencies = new [] { "dotvvm", "dotvvm.contrib.GoogleMap.GoogleCode", "dotvvm.contrib.GoogleMapCss" }
            });
            // register additional resources for the control and set up dependencies
            config.Resources.Register("dotvvm.contrib.GoogleMapCss", new StylesheetResource()
            {
                Location = new EmbeddedResourceLocation(typeof(GoogleMap).GetTypeInfo().Assembly, "DotVVM.Contrib.GoogleMap.Styles.DotVVM.Contrib.GoogleMap.css")
            });

        }

    }
}
