using System.Reflection;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.Contrib.MultilevelMenu
{
    public static class MultilevelMenuConfigurationExtensions
    {

        public static void AddContribMultilevelMenuConfiguration(this DotvvmConfiguration config)
        {
            config.Markup.Controls.Add(new DotvvmControlConfiguration()
            {
                Assembly = typeof(MultilevelMenu).Assembly.GetName().Name,
                Namespace = typeof(MultilevelMenu).Namespace,
                TagPrefix = "dc"
            });

            //// register additional resources for the control and set up dependencies
            config.Resources.Register("dotvvm.contrib.MultilevelMenu", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(typeof(MultilevelMenu).GetTypeInfo().Assembly, "DotVVM.Contrib.MultilevelMenu.Scripts.DotVVM.Contrib.MultilevelMenu.js"),
                Dependencies = new[] { "dotvvm", "dotvvm.contrib.MultilevelMenu.css" }
            });
            config.Resources.Register("dotvvm.contrib.MultilevelMenu.css", new StylesheetResource()
            {
                Location = new EmbeddedResourceLocation(typeof(MultilevelMenu).GetTypeInfo().Assembly, "DotVVM.Contrib.MultilevelMenu.Styles.DotVVM.Contrib.MultilevelMenu.css")
            });

            //// NOTE: all resource names should start with "dotvvm.contrib.MultilevelMenu"
        }

    }
}
