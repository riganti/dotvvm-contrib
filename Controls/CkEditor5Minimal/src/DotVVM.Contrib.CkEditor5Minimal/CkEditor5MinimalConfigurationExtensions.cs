using System.Reflection;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.Contrib.CkEditor5Minimal
{
    public static class CkEditor5MinimalConfigurationExtensions
    {
        public const string CkEditorResourceName = "dotvvm.contrib.CkEditor5Minimal.ckeditor5";
        public const string CkEditorStylesResourceName = "dotvvm.contrib.CkEditor5Minimal.ckeditor5.css";

        public static void AddContribCkEditor5MinimalConfiguration(this DotvvmConfiguration config)
        {
            config.Markup.Controls.Add(new DotvvmControlConfiguration()
            {
                Assembly = typeof(CkEditor5Minimal).Assembly.GetName().Name,
                Namespace = typeof(CkEditor5Minimal).Namespace,
                TagPrefix = "dc"
            });

            config.Resources.Register(CkEditorResourceName, new ScriptResource(
                new UrlResourceLocation("https://cdn.jsdelivr.net/npm/ckeditor5@48.4.0/dist/browser/ckeditor5.umd.js"))
            {
                IntegrityHash = "sha256-/XfpTfOrK0XdGMM+jN432Kp+VPiAY5g2RMwEqI6vVmk="
			});
            config.Resources.Register(CkEditorStylesResourceName, new StylesheetResource(
                new UrlResourceLocation("https://cdn.jsdelivr.net/npm/ckeditor5@48.4.0/dist/browser/ckeditor5.css"))
            {
                IntegrityHash = "sha256-906EVl8OA2UjlxF+AMWI6nEESnpbs+kMBlKSLrouek4="
			});

            config.Resources.Register("dotvvm.contrib.CkEditor5Minimal", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(typeof(CkEditor5Minimal).GetTypeInfo().Assembly, "DotVVM.Contrib.CkEditor5Minimal.Scripts.DotVVM.Contrib.CkEditor5Minimal.js"),
                Dependencies = new[] { "dotvvm", CkEditorResourceName, CkEditorStylesResourceName }
            });
        }

    }
}
