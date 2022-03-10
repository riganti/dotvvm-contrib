using System.Reflection;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.Contrib.TypeAhead
{
    public static class DotvvmConfigurationExtensions
    {

        public static void AddContribTypeAheadConfiguration(this DotvvmConfiguration config)
        {
            config.Markup.Controls.Add(new DotvvmControlConfiguration()
            {
                Assembly = typeof(TypeAhead).Assembly.GetName().Name,
                Namespace = typeof(TypeAhead).Namespace,
                TagPrefix = "dc"
            });

            // register additional resources for the control and set up dependencies
            if (config.Resources.FindResource("jquery") == null)
            {
                config.Resources.Register("jquery", new ScriptResource(new UrlResourceLocation("https://code.jquery.com/jquery-3.3.1.min.js")));
            }

            config.Resources.Register("typeahead", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(typeof(TypeAhead).GetTypeInfo().Assembly, "DotVVM.Contrib.TypeAhead.Scripts.typeahead.bundle.min.js"),
                Dependencies = new[] { "jquery" }
            });
            config.Resources.Register("dotvvm.contrib.TypeAhead", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(typeof(TypeAhead).GetTypeInfo().Assembly, "DotVVM.Contrib.TypeAhead.Scripts.DotVVM.Contrib.TypeAhead.js"),
                Dependencies = new[] { "dotvvm", "typeahead", "dotvvm.contrib.TypeAhead.css" }
            });
            config.Resources.Register("dotvvm.contrib.TypeAhead.css", new StylesheetResource()
            {
                Location = new EmbeddedResourceLocation(typeof(TypeAhead).GetTypeInfo().Assembly, "DotVVM.Contrib.TypeAhead.Styles.DotVVM.Contrib.TypeAhead.css")
            });

            // NOTE: all resource names should start with "dotvvm.contrib.TypeAhead"
        }

    }
}
