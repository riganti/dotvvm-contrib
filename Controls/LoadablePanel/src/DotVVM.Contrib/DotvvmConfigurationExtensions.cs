using System.Linq;
using System.Reflection;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.Contrib
{
    public static class DotvvmConfigurationExtensions
    {
        public const string JavascriptResourceName = "dotvvm.contrib.LoadablePanel";

        public static void AddContribLoadablePanelConfiguration(this DotvvmConfiguration config)
        {
            config.Markup.Controls.Add(new DotvvmControlConfiguration()
            {
                Assembly = typeof(LoadablePanel).Assembly.GetName().Name,
                Namespace = typeof(LoadablePanel).Namespace,
                TagPrefix = "dc"
            });

            // register additional resources for the control and set up dependencies
            config.Resources.Register(JavascriptResourceName, new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(typeof(LoadablePanel).GetTypeInfo().Assembly, "DotVVM.Contrib.Scripts.DotVVM.Contrib.LoadablePanel.js"),
                Dependencies = new [] { "dotvvm" }
            });
        }
    }
}
