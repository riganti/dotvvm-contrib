using System.Reflection;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.Contrib
{
    public static class CkEditorMinimalDotvvmConfigurationExtensions
    {
        public static void AddContribCkEditorMinimalConfiguration(this DotvvmConfiguration config)
        {
            // register tag prefix
            config.Markup.Controls.Add(new DotvvmControlConfiguration()
            {
                Assembly = typeof(CkEditorMinimal.CkEditorMinimal).Assembly.GetName().Name,
                Namespace = typeof(CkEditorMinimal.CkEditorMinimal).Namespace,
                TagPrefix = "dc"
            });
            // register additional resources for the control and set up dependencies
            config.Resources.Register("dotvvm.contrib.CkEditorMinimal", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(typeof(CkEditorMinimal.CkEditorMinimal).GetTypeInfo().Assembly, "DotVVM.Contrib.CkEditorMinimal.Scripts.DotVVM.Contrib.CkEditorMinimal.js"),
                Dependencies = new[] { "dotvvm" }
            });

            // NOTE: all resource names should start with "dotvvm.contrib.CkEditorMinimal"
        }
    }
}