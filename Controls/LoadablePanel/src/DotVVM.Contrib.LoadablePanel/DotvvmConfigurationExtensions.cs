using System.Reflection;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.Contrib.LoadablePanel
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

            var assemblyName = typeof(LoadablePanel).GetTypeInfo().Assembly;
            var scriptPath = "DotVVM.Contrib.LoadablePanel.Scripts.DotVVM.Contrib.LoadablePanel.js";

            // register additional resources for the control and set up dependencies
            config.Resources.Register(
                JavascriptResourceName,
                new ScriptResource(new EmbeddedResourceLocation(assemblyName, scriptPath), true)
            {
                Dependencies = new [] { "dotvvm" }
            });
        }
    }
}
