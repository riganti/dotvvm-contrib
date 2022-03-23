using System.Reflection;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.Contrib.Select2
{
    public static class DotvvmConfigurationExtensions
    {

        public static void AddContribSelect2Configuration(this DotvvmConfiguration config)
        {
            // register tag prefix
            var assembly = typeof(Contrib.Select2.Select2).Assembly.GetName().Name;
            config.Markup.Controls.Add(new DotvvmControlConfiguration()
            {
                Assembly = assembly,
                Namespace = typeof(Contrib.Select2.Select2).Namespace,
                TagPrefix = "dc"
            });

            // register additional resources for the control and set up dependencies
            config.Resources.Register("select2", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(typeof(Contrib.Select2.Select2).GetTypeInfo().Assembly, $"{assembly}.Scripts.select2.select2.min.js"),
                Dependencies = new[] { "dotvvm", "dotvvm.contrib.select2.css", "jquery" }
            });

            config.Resources.Register("dotvvm.contrib.select2.css", new StylesheetResource()
            {
                Location = new EmbeddedResourceLocation(typeof(Contrib.Select2.Select2).GetTypeInfo().Assembly, $"{assembly}.Styles.select2.select2.min.css")
            });

            config.Resources.Register("dotvvm.contrib.Select2", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(typeof(Contrib.Select2.Select2).GetTypeInfo().Assembly, $"{assembly}.Scripts.DotVVM.Contrib.Select2.js"),
                Dependencies = new[] { "select2" }
            });
        }
    }
}
