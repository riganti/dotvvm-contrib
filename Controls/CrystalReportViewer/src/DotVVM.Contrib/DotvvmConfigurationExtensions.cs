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
            config.Markup.Controls.Add(new DotvvmControlConfiguration()
            {
                Assembly = typeof(CrystalReportViewer).Assembly.GetName().Name,
                Namespace = typeof(CrystalReportViewer).Namespace,
                TagPrefix = "dc"
            });

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
