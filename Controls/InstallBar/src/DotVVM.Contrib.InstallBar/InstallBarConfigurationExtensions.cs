using System.IO;
using System.Reflection;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.Contrib.InstallBar
{
    public static class InstallBarConfigurationExtensions
    {
        internal const string EarlyCaptureResourceName = "dotvvm.contrib.InstallBar.early";

        public static void AddContribInstallBarConfiguration(this DotvvmConfiguration config)
        {
            var assembly = typeof(InstallBar).GetTypeInfo().Assembly;

            config.Markup.AddMarkupControl("dc", nameof(InstallBar), $"embedded://{assembly.GetName().Name}/{nameof(InstallBar)}.dotcontrol");

            config.Resources.Register("dotvvm.contrib.InstallBar.css", new StylesheetResource()
            {
                Location = new EmbeddedResourceLocation(assembly, "DotVVM.Contrib.InstallBar.Styles.DotVVM.Contrib.InstallBar.css")
            });

            config.Resources.Register("dotvvm.contrib.InstallBar", new ScriptModuleResource(new EmbeddedResourceLocation(assembly, "DotVVM.Contrib.InstallBar.Scripts.DotVVM.Contrib.InstallBar.js"))
            {
                Dependencies = new[] { "dotvvm", "dotvvm.contrib.InstallBar.css", EarlyCaptureResourceName }
            });

            // captures the beforeinstallprompt event before the page and its view modules are initialized
            config.Resources.Register(EarlyCaptureResourceName, new InlineScriptResource(ReadEmbeddedScript(assembly, "DotVVM.Contrib.InstallBar.Scripts.DotVVM.Contrib.InstallBar.Early.js"), ResourceRenderPosition.Head, defer: false));
        }

        private static string ReadEmbeddedScript(Assembly assembly, string name)
        {
            using (var stream = assembly.GetManifestResourceStream(name))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
