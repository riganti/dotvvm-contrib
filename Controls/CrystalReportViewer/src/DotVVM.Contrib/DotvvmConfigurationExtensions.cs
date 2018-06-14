using System.Linq;
using System.Reflection;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.Contrib
{
    public static class DotvvmConfigurationExtensions
    {
        public static void AddCrystalReportViewerConfiguration(this DotvvmConfiguration config)
        {
            // register tag prefix
            if (!config.Markup.Controls.Any(c => c.TagPrefix == "dc"))
            {
                config.Markup.Controls.Add(new DotvvmControlConfiguration()
                {
                    Assembly = "DotVVM.Contrib",
                    Namespace = "DotVVM.Contrib",
                    TagPrefix = "dc"
                });
            }

            config.Resources.Register("dotvvm.contrib.CrystalReportViewer", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(typeof(CrystalReportViewer).GetTypeInfo().Assembly, "DotVVM.Contrib.Scripts.DotVVM.Contrib.CrystalReportViewer.js"),
                Dependencies = new[] { "dotvvm", "dotvvm.contrib.CrystalReportViewer.css" }
            });

            config.Resources.Register("dotvvm.contrib.CrystalReportViewer.css", new StylesheetResource()
            {
                Location = new EmbeddedResourceLocation(typeof(CrystalReportViewer).GetTypeInfo().Assembly, "DotVVM.Contrib.Styles.DotVVM.Contrib.CrystalReportViewer.css")
            });

        }
    }
}
