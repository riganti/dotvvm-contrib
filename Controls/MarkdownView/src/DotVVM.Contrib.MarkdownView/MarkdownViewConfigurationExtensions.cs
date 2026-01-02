using System.Reflection;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.Contrib.MarkdownView
{
    public static class MarkdownViewConfigurationExtensions
    {

        public static void AddContribMarkdownViewConfiguration(this DotvvmConfiguration config)
        {
            config.Markup.Controls.Add(new DotvvmControlConfiguration()
            {
                Assembly = typeof(MarkdownView).Assembly.GetName().Name,
                Namespace = typeof(MarkdownView).Namespace,
                TagPrefix = "dc"
            });

            // register additional resources for the control and set up dependencies
            config.Resources.Register("dotvvm.contrib.MarkdownView", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(typeof(MarkdownView).GetTypeInfo().Assembly, "DotVVM.Contrib.MarkdownView.Scripts.DotVVM.Contrib.MarkdownView.js"),
                Dependencies = new [] { "dotvvm", "dotvvm.contrib.MarkdownView.css", "showdown" }
            });
            config.Resources.Register("dotvvm.contrib.MarkdownView.css", new StylesheetResource()
            {
                Location = new EmbeddedResourceLocation(typeof(MarkdownView).GetTypeInfo().Assembly, "DotVVM.Contrib.MarkdownView.Styles.DotVVM.Contrib.MarkdownView.css")
            });

            config.Resources.Register("showdown", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(typeof(MarkdownView).GetTypeInfo().Assembly, "DotVVM.Contrib.MarkdownView.Scripts.showdown.min.js")
            });
        }

    }
}
