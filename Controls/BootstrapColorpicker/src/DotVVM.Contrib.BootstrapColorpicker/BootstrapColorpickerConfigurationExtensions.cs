using System.Reflection;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.Contrib.BootstrapColorpicker
{
    public static class BootstrapColorpickerConfigurationExtensions
    {

        public static void AddContribBootstrapColorpickerConfiguration(this DotvvmConfiguration config)
        {
            config.Markup.Controls.Add(new DotvvmControlConfiguration()
            {
                Assembly = typeof(BootstrapColorpicker).Assembly.GetName().Name,
                Namespace = typeof(BootstrapColorpicker).Namespace,
                TagPrefix = "dc"
            });
            
            // register additional resources for the control and set up dependencies
            config.Resources.Register("dotvvm.contrib.BootstrapColorpicker", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(typeof(BootstrapColorpicker).GetTypeInfo().Assembly, "DotVVM.Contrib.BootstrapColorpicker.Scripts.DotVVM.Contrib.BootstrapColorpicker.js"),
                Dependencies = new [] { "dotvvm", "BootstrapColorpicker", "BootstrapColorpicker.css" }
            });
            config.Resources.Register("BootstrapColorpicker", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(typeof(BootstrapColorpicker).GetTypeInfo().Assembly, "DotVVM.Contrib.BootstrapColorpicker.Scripts.bootstrap-colorpicker.min.js"),
                Dependencies = new[] { "bootstrap" }
            });
            config.Resources.Register("BootstrapColorpicker.css", new StylesheetResource()
            {
                Location = new EmbeddedResourceLocation(typeof(BootstrapColorpicker).GetTypeInfo().Assembly, "DotVVM.Contrib.BootstrapColorpicker.Styles.bootstrap-colorpicker.min.css")
            });
        }

    }
}
