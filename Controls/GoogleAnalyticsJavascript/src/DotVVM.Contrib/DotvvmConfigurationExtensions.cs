using System.Linq;
using DotVVM.Framework.Configuration;

namespace DotVVM.Contrib
{
    public static class DotvvmConfigurationExtensions
    {

        public static void AddContribGoogleAnalyticsJavascriptConfiguration(this DotvvmConfiguration config)
        {
            // register tag prefix
            config.Markup.Controls.Add(new DotvvmControlConfiguration()
            {
                Assembly = "DotVVM.Contrib.GoogleAnalyticsJavascript",
                Namespace = "DotVVM.Contrib",
                TagPrefix = "dc"
            });            
        }

    }
}
